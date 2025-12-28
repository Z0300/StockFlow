using StockFlow.Application.Abstractions.Caching;
using StockFlow.Application.Suppliers.Shared;

namespace StockFlow.Application.Suppliers.GetById;

public sealed record GetSupplierByIdQuery(Guid SupplierId) : ICachedQuery<SupplierResponse>
{
    public string CacheKey => $"suppliers-{SupplierId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
