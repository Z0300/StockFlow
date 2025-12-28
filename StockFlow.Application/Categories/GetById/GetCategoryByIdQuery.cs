using StockFlow.Application.Abstractions.Caching;
using StockFlow.Application.Categories.Shared;

namespace StockFlow.Application.Categories.GetById;

public sealed record GetCategoryByIdQuery(Guid CategoryId) : ICachedQuery<CategoryResponse>
{
    public string CacheKey => $"categories-{CategoryId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
