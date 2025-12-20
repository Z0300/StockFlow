using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Domain.Enums;
using StockFlow.Domain.Exceptions;

namespace StockFlow.Application.Transactions.Create.PolicyResolver;

internal sealed class ConsumptionPolicy : ITransactionPolicyResolver
{
    public TransactionType Type => TransactionType.Consumption;

    public Task ValidateAsync(CreateTransactionCommand command, CancellationToken ct)
    {
        if (command.OrderId is not null)
        {
            throw new DomainException("Consumption must not reference an order");
        }

        if (command.Items.Any(item => item.QuantityChange >= 0))
        {
            throw new DomainException("Consumption must reduce stock");
        }

        return Task.CompletedTask;
    }
}
