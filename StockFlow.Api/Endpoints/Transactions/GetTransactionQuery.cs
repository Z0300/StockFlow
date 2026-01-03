using StockFlow.Domain.Entities.Transactions.Enums;

namespace StockFlow.Api.Endpoints.Transactions;

public sealed record RequestGetTransactionQuery
{
    public required DateOnly From { get; init; }
    public required DateOnly To { get; init; }
    public int Type { get; init; } = 1;
    public string ProductName { get; set; } = string.Empty;

    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}
