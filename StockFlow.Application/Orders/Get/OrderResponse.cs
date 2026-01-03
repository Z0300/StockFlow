using StockFlow.Domain.Entities.Orders.Enums;

namespace StockFlow.Application.Orders.Get;

public class OrdersResponse
{
    public Guid OrderId { get; init; }
    public DateTime OrderDate { get; init; }
    public DateTime? ReceivedDate { get; init; }
    public OrderStatus OrderStatus { get; init; }
}


