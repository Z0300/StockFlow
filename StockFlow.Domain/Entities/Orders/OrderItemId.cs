namespace StockFlow.Domain.Entities.Orders;

public record OrderItemId(Guid Value)
{
    public static OrderItemId New() => new(Guid.NewGuid());
}
