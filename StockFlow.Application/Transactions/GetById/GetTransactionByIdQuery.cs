using StockFlow.Application.Abstractions.Caching;

namespace StockFlow.Application.Transactions.GetById;

public sealed record GetTransactionByIdQuery(Guid TransactionId) : ICachedQuery<TransactionResponse>
{
    public string CacheKey => $"transactions-{TransactionId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
