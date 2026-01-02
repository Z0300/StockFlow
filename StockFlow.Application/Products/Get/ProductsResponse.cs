namespace StockFlow.Application.Products.Get;

public sealed class ProductsResponse
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
    public string ProductSku { get; init; }
    public decimal ProductPrice { get; init; }
    public string ProductCurrency { get; init; }
};
