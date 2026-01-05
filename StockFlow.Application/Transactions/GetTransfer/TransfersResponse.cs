namespace StockFlow.Application.Transactions.GetTransfer;

public sealed class TransfersResponse
{
    public Guid TransferId { get; init; }
    public string SourceWarehouse { get; init; }
    public string DestinationWarehouse { get; init; }
    public int Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? ConfirmAt { get; init; }
    public DateTime? ReceiveAt { get; init; }
}
