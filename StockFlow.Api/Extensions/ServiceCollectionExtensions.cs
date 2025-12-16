using Microsoft.OpenApi;

namespace StockFlow.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(static o =>
        {
            o.CustomSchemaIds(id => id.FullName?.Replace('+', '-'));

            o.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme."
            });

            o.AddSecurityRequirement(d => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("bearer", d)] = []
            });
        });

        return services;
    }
}
