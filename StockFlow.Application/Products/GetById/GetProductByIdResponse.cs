using StockFlow.Application.Categories.Shared;

namespace StockFlow.Application.Products.GetById;

public sealed class GetProductByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public CategoryResponse Category { get; set; }
}
