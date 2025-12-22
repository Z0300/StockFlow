using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Domain.Enums;
using StockFlow.Domain.Exceptions;

namespace StockFlow.Application.Transactions.Create.PolicyResolver;

internal sealed class PurchaseReceiptPolicy : ITransactionPolicyResolver
{
    public TransactionType Type => TransactionType.PurchaseReceipt;

    public Task ValidateAsync(CreateTransactionCommand command, CancellationToken ct)
    {

        return Task.CompletedTask;
    }
}
