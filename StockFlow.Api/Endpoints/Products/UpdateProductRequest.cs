namespace StockFlow.Api.Endpoints.Products;

public sealed record UpdateProductRequest
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Sku { get; init; }
    public required decimal Price { get; init; }
    public required Guid CategoryId { get; init; }
}

