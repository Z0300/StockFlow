using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Entities.Suppliers;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Infrastructure.Repositories;

internal sealed class SupplierRepository : Repository<Supplier, SupplierId>, ISupplierRepository
{
    public SupplierRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<bool> IsNameUnique(string name, CancellationToken cancellationToken = default)
    {
        return await DbContext
           .Set<Supplier>()
           .AnyAsync(supplier => supplier.Name == name, cancellationToken);
    }
}
