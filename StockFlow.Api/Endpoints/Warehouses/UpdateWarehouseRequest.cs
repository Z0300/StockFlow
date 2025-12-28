namespace StockFlow.Api.Endpoints.Warehouses;

public sealed record UpdateWarehouseRequest
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Location { get; init; }
}

