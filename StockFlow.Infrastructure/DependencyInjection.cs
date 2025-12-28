using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using StockFlow.Application.Abstractions.Authentication;
using StockFlow.Application.Abstractions.Caching;
using StockFlow.Application.Abstractions.Clock;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Categories;
using StockFlow.Domain.Entities.Orders;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Suppliers;
using StockFlow.Domain.Entities.Transactions;
using StockFlow.Domain.Entities.Transfers;
using StockFlow.Domain.Entities.Users;
using StockFlow.Domain.Entities.Warehouses;
using StockFlow.Infrastructure.Authentication;
using StockFlow.Infrastructure.Authorization;
using StockFlow.Infrastructure.Caching;
using StockFlow.Infrastructure.Database;
using StockFlow.Infrastructure.Outbox;
using StockFlow.Infrastructure.Repositories;
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
                .AddAuthorizationInternal()
                .AddBackgroundJobs(configuration)
                .AddCaching(configuration);

        private IServiceCollection AddServices(IConfiguration configuration)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
          
            return services;
        }

        private IServiceCollection AddBackgroundJobs(IConfiguration configuration)
        {
            services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

            services.AddQuartz(configurator =>
            {
                var schedulerId = Guid.NewGuid();
                configurator.SchedulerId = $"default-id-{schedulerId}";
                configurator.SchedulerName = $"default-name-{schedulerId}";
            });

            services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

            services.ConfigureOptions<ProcessOutboxMessageJobSetup>();

            return services;
        }

        private IServiceCollection AddDatabase(IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("Database") ??
                 throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(
                options => options
                    .UseNpgsql(connectionString, npgsqlOptions =>
                        npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName))
                    .UseSnakeCaseNamingConvention());

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

            #region Repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ITransferRepository, TransferRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion


            return services;
        }

        private IServiceCollection AddCaching(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Cache") ??
                                    throw new ArgumentNullException(nameof(configuration));

            services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

            services.AddSingleton<ICacheService, CacheService>();

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
