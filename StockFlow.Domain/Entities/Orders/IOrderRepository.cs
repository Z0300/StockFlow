using StockFlow.Domain.Entities.Products;

namespace StockFlow.Domain.Entities.Orders;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(OrderId id, CancellationToken cancellationToken = default);
    void Add(Order order);
}
