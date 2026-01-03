using StockFlow.Application.Abstractions.Caching;

namespace StockFlow.Application.Products.GetById;

public sealed record GetProductByIdQuery(Guid ProductId) : ICachedQuery<ProductResponse>
{
    public string CacheKey => $"prodcuts-{ProductId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
