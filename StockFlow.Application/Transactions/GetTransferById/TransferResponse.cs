namespace StockFlow.Application.Transactions.GetTransferById;

public sealed class TransferResponse
{
    public Guid TransferId { get; init; }
    public string SourceWarehouse { get; init; }
    public string DestinationWarehouse { get; init; }
    public int Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ConfirmAt { get; init; }
    public DateTime? ReceiveAt { get; init; }
    public List<TransferItemsResponse> Items { get; set; }
}
