namespace StockFlow.Domain.Entities.TransactionItems;

public sealed record TransactionItemId(Guid Value)
{
    public static TransactionItemId New() => new(Guid.NewGuid());
}
