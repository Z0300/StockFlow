using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Domain.Enums;
using StockFlow.Domain.Exceptions;

namespace StockFlow.Application.Transactions.Create.PolicyResolver;

internal sealed class SaleIssuePolicy : ITransactionPolicyResolver
{
    public TransactionType Type => TransactionType.SaleIssue;

    public Task ValidateAsync(CreateTransactionCommand command, CancellationToken ct)
    {
        if (command.OrderId is null || command.OrderId == Guid.Empty)
        {
            throw new DomainException("OrderId required");
        }

        if (command.Items.Any(item => item.QuantityChange >= 0))
        {
            throw new DomainException("Sale must reduce stock");
        }

        return Task.CompletedTask;
    }
}
