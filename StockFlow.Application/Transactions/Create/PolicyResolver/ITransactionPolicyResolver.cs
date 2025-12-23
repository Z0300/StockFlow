using StockFlow.Domain.Enums;

namespace StockFlow.Application.Transactions.Create.PolicyResolver;

internal interface ITransactionPolicyResolver
{
    TransactionType Type { get; }
    Task ValidateAsync(CreateTransactionCommand command, CancellationToken ct);
}
