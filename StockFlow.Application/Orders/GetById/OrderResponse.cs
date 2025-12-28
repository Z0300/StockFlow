using StockFlow.Application.Suppliers.Shared;
using StockFlow.Application.Warehouses.Shared;

namespace StockFlow.Application.Orders.GetById;

public sealed class OrderResponse
{
    public Guid OrderId { get; init; }
    public DateTime OrderDate { get; init; }
    public WarehouseResponse Warehouse { get; set; }
    public SupplierResponse Supplier { get; set; } 
    public decimal TotalAmount { get; init; }
    public string Status { get; init; }
    public List<OrderItemResponse> OrderItems { get; set; } = [];
}

public sealed class OrderItemResponse
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; } 
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
}
