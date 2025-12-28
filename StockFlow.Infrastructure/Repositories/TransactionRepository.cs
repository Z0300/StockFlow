using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Transactions;
using StockFlow.Domain.Entities.Warehouses;
using StockFlow.Infrastructure.Database;

namespace StockFlow.Infrastructure.Repositories;

internal sealed class TransactionRepository : Repository<Transaction, TransactionId>, ITransactionRepository
{
    public TransactionRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task BulkInsertAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken)
    {
        await DbContext.BulkInsertOptimizedAsync(transactions, 
            options =>  options.IncludeGraph = true,
            cancellationToken: cancellationToken);
    }

    public async Task<int> GetAvailableQuantity(WarehouseId warehouseId, ProductId productId, CancellationToken cancellationToken)
    {
       return await DbContext
            .Set<Transaction>()
            .Where(t => t.WarehouseId == warehouseId && t.ProductId == productId)
            .SumAsync(t => t.QuantityChange, cancellationToken);
    }
}
