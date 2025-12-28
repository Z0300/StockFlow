using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Entities.Warehouses;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Infrastructure.Repositories;

internal sealed class WarehouseRepository : Repository<Warehouse, WarehouseId>, IWarehouseRepository
{
    public WarehouseRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<bool> IsNameUnique(string name, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Warehouse>()
            .AnyAsync(category => category.Name == name, cancellationToken);
    }
}
