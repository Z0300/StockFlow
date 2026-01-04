namespace StockFlow.Application.Transactions.GetById;

public sealed class TransactionItemsResponse
{
    public Guid TransactionItemId { get; set; }
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
    public int QuantityChange { get; init; }
    public decimal? UnitCost { get; init; }
    public string Currency { get; init; }
}
