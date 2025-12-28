namespace StockFlow.Application.Warehouses.Shared;

public sealed class WarehouseResponse
{
    public Guid WarehouseId { get; init; }
    public string WarehouseName { get; init; }
    public string WarehouseLocation { get; init; }
}
