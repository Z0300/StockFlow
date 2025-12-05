using SharedKernel;
using StockFlow.Domain.Enums;
using StockFlow.Domain.OrderItems;
using StockFlow.Domain.Suppliers;
using StockFlow.Domain.Warehouses;

namespace StockFlow.Domain.Orders;

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