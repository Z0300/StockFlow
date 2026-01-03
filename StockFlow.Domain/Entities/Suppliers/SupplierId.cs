namespace StockFlow.Domain.Entities.Suppliers;

public record SupplierId(Guid Value)
{
    public static SupplierId New() => new(Guid.NewGuid());
}
