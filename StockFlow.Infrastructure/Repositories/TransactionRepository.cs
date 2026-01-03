using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.TransactionItems;
using StockFlow.Domain.Entities.Transactions;
using StockFlow.Domain.Entities.Warehouses;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Infrastructure.Repositories;

internal sealed class TransactionRepository : Repository<Transaction, TransactionId>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<int> GetAvailableQuantity(WarehouseId warehouseId, ProductId productId, CancellationToken cancellationToken)
    {
        return await DbContext
             .Set<TransactionItem>()
             .Include(ti => ti.Transaction)
             .Where(ti => ti.Transaction.WarehouseId == warehouseId && ti.ProductId == productId)
             .SumAsync(ti => ti.QuantityChange, cancellationToken);

    }
}
