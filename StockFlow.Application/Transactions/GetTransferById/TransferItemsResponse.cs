namespace StockFlow.Application.Transactions.GetTransferById;

public sealed class TransferItemsResponse
{
    public Guid TransferItemId { get; init; }
    public string Product { get; init; }
    public int RequestedQuantity { get; init; }
    public int? ReceivedQuantity { get; init; }
}
