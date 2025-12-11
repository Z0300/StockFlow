using System.Reflection;
using StockFlow.Api;
using StockFlow.Api.Extensions;
using StockFlow.Application;
using StockFlow.Infrastructure;
using Serilog;
using Serilog.AspNetCore; // Add this using directive

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => 
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGenWithAuth();

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

WebApplication app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
    app.MapOpenApi();
}

app.UseRequestContextLogging();

app.UseSerilogRequestLogging(); // This line will now work if Serilog.AspNetCore is referenced

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

 await app.RunAsync();
