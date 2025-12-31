using StockFlow.Application.Suppliers.Shared;
using StockFlow.Application.Warehouses.Shared;
using StockFlow.Domain.Entities.Orders.Enums;

namespace StockFlow.Application.Orders.GetById;

public sealed class OrderResponse
{
    public Guid OrderId { get; init; }
    public DateTime OrderDate { get; init; }
    public WarehouseResponse Warehouse { get; set; }
    public SupplierResponse Supplier { get; set; } 
    public OrderStatus OrderStatus { get; init; }
    public List<OrderItemResponse> OrderItems { get; set; } = [];
}

public sealed class OrderItemResponse
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } 
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public string Currency { get; set; }
}
