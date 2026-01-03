using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Products;
using StockFlow.Domain.Shared;

namespace StockFlow.Domain.Entities.Orders;

public class OrderItem : Entity<OrderItemId>
{
    private OrderItem(
        OrderItemId id,
        ProductId productId,
        int quantity,
        Money unitPrice) : base(id)
    {
        Id = id;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    protected OrderItem() { }

    public OrderId OrderId { get; protected set; }
    public ProductId ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }

    public static OrderItem Create(
        ProductId productId,
        int quantity,
        Money unitPrice)
    {
        return new OrderItem(
            OrderItemId.New(),
            productId,
            quantity,
            unitPrice);
    }
}
