using Microsoft.EntityFrameworkCore;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Api.Extensions;


public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}