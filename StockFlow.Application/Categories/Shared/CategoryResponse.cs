namespace StockFlow.Application.Categories.Shared;

public sealed class CategoryResponse
{
    public Guid CategoryId { get; init; }
    public string CategoryName { get; init; }
    public string CategoryDescription { get; init; }
}
