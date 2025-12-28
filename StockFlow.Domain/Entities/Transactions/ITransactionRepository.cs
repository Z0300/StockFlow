using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Warehouses;

namespace StockFlow.Domain.Entities.Transactions;

public interface ITransactionRepository
{
    Task BulkInsertAsync(IEnumerable<Transaction> transactions, CancellationToken cancellationToken);
    Task<int> GetAvailableQuantity(WarehouseId warehouseId, ProductId productId, CancellationToken cancellationToken);
}
