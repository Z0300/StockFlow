using StockFlow.Application.Suppliers.Shared;

namespace StockFlow.Application.Orders.Get;

public sealed class OrdersResponse
{
    public Guid OrderId { get; init; }
    public DateTime OrderDate { get; init; }
    public decimal OrderTotalAmount { get; init; }
    public int OrderStatus { get; init; }
}
