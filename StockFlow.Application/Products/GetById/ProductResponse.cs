using StockFlow.Application.Categories.Shared;

namespace StockFlow.Application.Products.GetById;

public sealed class ProductResponse
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductSku { get; set; }
    public decimal ProductPrice { get; set; }
    public CategoryResponse Category { get; set; }
}
