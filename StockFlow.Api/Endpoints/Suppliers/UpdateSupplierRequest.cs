namespace StockFlow.Api.Endpoints.Suppliers;

public sealed record UpdateSupplierRequest
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string ContactInfo { get; init; }
}

