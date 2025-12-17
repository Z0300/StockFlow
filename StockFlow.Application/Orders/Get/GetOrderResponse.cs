namespace StockFlow.Application.Orders.Get;

public sealed class GetOrderResponse
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
}
