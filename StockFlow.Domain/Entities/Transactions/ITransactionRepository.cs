using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Warehouses;

namespace StockFlow.Domain.Entities.Transactions;

public interface ITransactionRepository
{
    void Add(Transaction transaction);
    Task<int> GetAvailableQuantity(WarehouseId warehouseId, ProductId productId, CancellationToken cancellationToken);
}
