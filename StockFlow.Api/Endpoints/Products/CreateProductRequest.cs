namespace StockFlow.Api.Endpoints.Products;

public sealed record CreateProductRequest
{
    public required string Name { get; init; }
    public required string Sku { get; init; }
    public required decimal Price { get; init; }
    public required Guid CategoryId { get; init; }
}
