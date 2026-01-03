namespace StockFlow.Domain.Entities.TransferItems;

public record TransferItemId(Guid Value)
{
    public static TransferItemId New() => new(Guid.NewGuid());
}
