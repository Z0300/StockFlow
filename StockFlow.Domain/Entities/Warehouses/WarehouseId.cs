namespace StockFlow.Domain.Entities.Warehouses;

public record WarehouseId(Guid Value)
{
    public static WarehouseId New() => new(Guid.NewGuid());
}
