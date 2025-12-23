namespace StockFlow.Domain.Enums;

public enum TransferStatus
{
    Draft = 0,
    InTransit = 1,
    PartiallyReceived = 2,
    Completed = 3,
    Cancelled = 4
}
