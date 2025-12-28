using StockFlow.Application.Abstractions.Caching;
using StockFlow.Application.Warehouses.Shared;

namespace StockFlow.Application.Warehouses.GetById;

public sealed record GetWarehouseByIdQuery(Guid WarehouseId) : ICachedQuery<WarehouseResponse>
{
    public string CacheKey => $"warehouses-{WarehouseId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
