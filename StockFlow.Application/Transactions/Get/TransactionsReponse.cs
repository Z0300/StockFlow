using StockFlow.Domain.Entities.Transactions.Enums;

namespace StockFlow.Application.Transactions.Get;

public sealed class TransactionsReponse
{
    public Guid TransactionId { get; init; }
    public string Product { get; init; }
    public string Warehouse { get; init; }
    public TransactionType TransactionType { get; init; }
}
