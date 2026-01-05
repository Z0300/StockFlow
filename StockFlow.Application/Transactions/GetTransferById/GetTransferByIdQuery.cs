using StockFlow.Application.Abstractions.Caching;

namespace StockFlow.Application.Transactions.GetTransferById;

public sealed record GetTransferByIdQuery(Guid TransferId) : ICachedQuery<TransferResponse>
{
    public string CacheKey => $"transfers-{TransferId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
