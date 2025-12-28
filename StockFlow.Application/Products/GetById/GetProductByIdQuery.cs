using StockFlow.Application.Abstractions.Caching;
using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Products.GetById;

public sealed record GetProductByIdQuery(Guid ProductId) : ICachedQuery<ProductResponse>
{
    public string CacheKey => $"prodcuts-{ProductId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
