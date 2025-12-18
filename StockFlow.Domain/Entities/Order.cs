using SharedKernel;
using StockFlow.Domain.Enums;

namespace StockFlow.Domain.Entities;

public sealed class Order : Entity
{
    public Guid WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }

    public Guid SupplierId { get; set; }
    public Supplier? Supplier { get; set; }

    public OrderStatus OrderStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ReceivedAt { get; set; }

    public List<OrderItem> OrderItems { get; set; } = [];
}