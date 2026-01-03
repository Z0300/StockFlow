using StockFlow.Api.Middleware;

namespace StockFlow.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(o =>
        {
            o.SwaggerEndpoint("/swagger/v1/swagger.json", "StockFlow API V1");
            o.RoutePrefix = string.Empty;
            o.ConfigObject.DefaultModelsExpandDepth = 0;
        });

        return app;
    }

    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionHandlingMiddleware>();

    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
        => app.UseMiddleware<RequestContextLoggingMiddleware>();
}
