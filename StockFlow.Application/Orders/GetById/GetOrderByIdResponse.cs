namespace StockFlow.Application.Orders.GetById;

public sealed class GetOrderByIdResponse
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<OrderItemResponse> OrderItems { get; set; } = [];
}

public sealed class OrderItemResponse
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
