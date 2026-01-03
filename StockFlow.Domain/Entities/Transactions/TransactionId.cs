namespace StockFlow.Domain.Entities.Transactions;

public record TransactionId(Guid Value)
{
    public static TransactionId New() => new(Guid.NewGuid());
}

