using StockFlow.Application.Abstractions.Caching;

namespace StockFlow.Application.Orders.GetById;

public sealed record GetOrderByIdQuery(Guid OrderId) : ICachedQuery<OrderResponse>
{
    public string CacheKey => $"orders-{OrderId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
