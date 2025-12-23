using StockFlow.Domain.Enums;

namespace StockFlow.Application.Transactions.Create.PolicyResolver;

internal sealed class ReturnToSupplierPolicy : ITransactionPolicyResolver
{
    public TransactionType Type => TransactionType.ReturnToSupplier;

    public Task ValidateAsync(CreateTransactionCommand command, CancellationToken ct)
    {


        return Task.CompletedTask;
    }
}
