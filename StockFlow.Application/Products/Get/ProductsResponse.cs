namespace StockFlow.Application.Products.Get;

public class ProductsResponse
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductSku { get; set; }
    public decimal ProductPrice { get; set; }
};
