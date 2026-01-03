namespace StockFlow.Domain.Entities.Transfers;

public record TransferId(Guid Value)
{
    public static TransferId New() => new(Guid.NewGuid());
}
