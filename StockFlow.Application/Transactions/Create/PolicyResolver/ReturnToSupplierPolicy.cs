using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.Enums;
using StockFlow.Domain.Exceptions;

namespace StockFlow.Application.Transactions.Create.PolicyResolver;

internal sealed class ReturnToSupplierPolicy : ITransactionPolicyResolver
{
    public TransactionType Type => TransactionType.ReturnToSupplier;

    public Task ValidateAsync(CreateTransactionCommand command, CancellationToken ct)
    {
        foreach (TransactionItems item in command.Items)
        {
            if (item.QuantityChange >= 0)
            {
                throw new
                    DomainException("Return to supplier must reduce stock");
            }
        }

        if (command.OrderId is null || command.OrderId == Guid.Empty)
        {
            throw new DomainException(
                "Return to supplier must reference a purchase order");
        }

        if (string.IsNullOrWhiteSpace(command.Reason))
        {
            throw new DomainException(
                "Return to supplier requires a reason");
        }

        return Task.CompletedTask;
    }
}
