using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using SharedKernel;
using StockFlow.Application.Abstractions.Authentication;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Infrastructure.Authentication;
using StockFlow.Infrastructure.Authorization;
using StockFlow.Infrastructure.BackgroundJobs;
using StockFlow.Infrastructure.Database;
using StockFlow.Infrastructure.DomainEvents;
using StockFlow.Infrastructure.Interceptors;
using StockFlow.Infrastructure.Messaging;
using StockFlow.Infrastructure.Time;

namespace StockFlow.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure(IConfiguration configuration) =>
            services
                .AddServices(configuration)
                .AddDatabase(configuration)
                .AddAuthenticationInternal(configuration)
                .AddAuthorizationInternal();

        private IServiceCollection AddServices(IConfiguration configuration)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();

            // Interceptors

            services.AddScoped<OutboxSaveChangesInterceptor>();

            services.AddDbContext<ApplicationDbContext>((serviceProvider, optionsBuilder) =>
            {
                OutboxSaveChangesInterceptor interceptor = serviceProvider.GetRequiredService<OutboxSaveChangesInterceptor>();
                optionsBuilder.AddInterceptors(interceptor);
            });

            // Background Jobs

            services.AddScoped<IJob, ProcessOutboxMessagesJob>();
            services.AddScoped<IIntegrationEventPublisher, IntegrationEventPublisher>();

            services.AddMassTransit(x =>
            {
                string? rabbitMqHost = configuration["RabbitMq:rabbitmq"];
                string? rabbitMqUsername = configuration["RabbitMq:guest"];
                string? rabbitMqPassword = configuration["RabbitMq:guest"];

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(rabbitMqHost, "/", h =>
                    {
                        h.Username(rabbitMqUsername!);
                        h.Password(rabbitMqPassword!);
                    });
                });
            });

            // Quartz Configuration

            services.AddQuartz(config =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

                config
                    .AddJob<ProcessOutboxMessagesJob>(j => j.WithIdentity(jobKey))
                    .AddTrigger(trigger => trigger.ForJob(jobKey)
                        .WithSimpleSchedule(schedule =>
                            schedule.WithIntervalInSeconds(10)
                        .RepeatForever()));
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            return services;
        }

        private IServiceCollection AddDatabase(IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Database");

            services.AddDbContext<ApplicationDbContext>(
                options => options
                    .UseNpgsql(connectionString, npgsqlOptions =>
                        npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName))
                    .UseSnakeCaseNamingConvention());

            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

            return services;
        }

        private IServiceCollection AddAuthenticationInternal(IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddHttpContextAccessor();
            services.AddScoped<IUserContext, UserContext>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ITokenProvider, TokenProvider>();

            return services;
        }

        private IServiceCollection AddAuthorizationInternal()
        {
            services.AddAuthorization();

            services.AddScoped<PermissionProvider>();

            services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

            services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            return services;
        }
    }
}
