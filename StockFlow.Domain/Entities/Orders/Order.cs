using StockFlow.Domain.Entities.Abstractions;
using StockFlow.Domain.Entities.Orders.Enums;
using StockFlow.Domain.Entities.Suppliers;
using StockFlow.Domain.Entities.Warehouses;

namespace StockFlow.Domain.Entities.Orders;

public class Order : Entity<OrderId>
{
    public Order(
        OrderId id,
        WarehouseId warehouseId,
        SupplierId supplierId,
        OrderStatus orderStatus,
        DateTime createdAt,
        DateTime? receivedAt,
        List<OrderItem> items
        ) : base(id)
    {
        WarehouseId = warehouseId;
        SupplierId = supplierId;
        OrderStatus = orderStatus;
        CreatedAt = createdAt;
        ReceivedAt = receivedAt;
        OrderItems = items;
    }

    protected Order() { }

    public WarehouseId WarehouseId { get; private set; }
    public SupplierId SupplierId { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ReceivedAt { get; private set; }
    public List<OrderItem> OrderItems { get; private set; }

    public static Order Create(
        WarehouseId warehouseId,
        SupplierId supplierId,
        OrderStatus orderStatus,
        DateTime createdAt,
        List<OrderItem> items)
    {
        return new Order(
            OrderId.New(),
            warehouseId,
            supplierId,
            orderStatus,
            createdAt,
            null,
            items);
    }

    public Result MarkAsReceived(DateTime receivedAt)
    {
        OrderStatus = OrderStatus.Received;
        ReceivedAt = receivedAt;

        return Result.Success();
    }

    public Result Cancel()
    {

        if (OrderStatus == OrderStatus.Received)
        {
            return Result.Failure(OrderErrors.InvalidOperation);
        }

        OrderStatus = OrderStatus.Cancelled;
        return Result.Success();
    }
}
