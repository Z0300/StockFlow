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
        if (command.OrderId is null || command.OrderId == Guid.Empty)
        {
            throw new DomainException("OrderId required");
        }

        if (command.Items.Any(item => item.QuantityChange <= 0))
        {
            throw new DomainException("Receipt must increase stock");
        }

        return Task.CompletedTask;
    }
}
