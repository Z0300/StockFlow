using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Entities.Transactions.Enums;
using StockFlow.Domain.Shared;

namespace StockFlow.Application.Transactions.Get;

public sealed record GetTransactionQuery(
    DateOnly From,
    DateOnly To,
    string ProductName,
    int Type,
    int Page,
    int PageSize) : IQuery<PagedList<TransactionReponse>>;
