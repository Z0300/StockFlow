using StockFlow.Domain.Enums;

namespace StockFlow.Application.Transactions.Create.PolicyResolver;

internal sealed class ConsumptionPolicy : ITransactionPolicyResolver
{
    public TransactionType Type => TransactionType.Consumption;

    public Task ValidateAsync(CreateTransactionCommand command, CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}
