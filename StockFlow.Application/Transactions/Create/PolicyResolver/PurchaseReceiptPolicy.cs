using StockFlow.Domain.Enums;

namespace StockFlow.Application.Transactions.Create.PolicyResolver;

internal sealed class PurchaseReceiptPolicy : ITransactionPolicyResolver
{
    public TransactionType Type => TransactionType.PurchaseReceipt;

    public Task ValidateAsync(CreateTransactionCommand command, CancellationToken ct)
    {

        return Task.CompletedTask;
    }
}
