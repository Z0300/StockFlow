using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Entities.Suppliers;
using StockFlow.Domain.Entities.Warehouses;

namespace StockFlow.Domain.Entities.Orders;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default);
    void Add(Order order);
}
