using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Domain.Enums;
using StockFlow.Domain.Exceptions;

namespace StockFlow.Application.Transactions.Create.PolicyResolver;

internal sealed class CustomerReturnPolicy : ITransactionPolicyResolver
{
    public TransactionType Type => TransactionType.CustomerReturn;

    public Task ValidateAsync(CreateTransactionCommand command, CancellationToken ct)
    {
        if (command.OrderId is null || command.OrderId == Guid.Empty)
        {
            throw new DomainException("Customer return must reference a sales order");
        }

        if (command.Items.Any(item => item.QuantityChange <= 0))
        {
            throw new DomainException("Customer return must increase stock");
        }

        return Task.CompletedTask;
    }
}
