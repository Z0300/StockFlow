namespace StockFlow.Application.Products.Get;

public sealed class GetProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public string Warehouse { get; set; }
};
