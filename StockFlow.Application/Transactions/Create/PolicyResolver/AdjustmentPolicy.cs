using StockFlow.Domain.Enums;

namespace StockFlow.Application.Transactions.Create.PolicyResolver;

internal sealed class AdjustmentPolicy : ITransactionPolicyResolver
{
    public TransactionType Type => TransactionType.Adjustment;

    public Task ValidateAsync(CreateTransactionCommand command, CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
