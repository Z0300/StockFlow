using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Application.Transactions.Create;
using StockFlow.Domain.Enums;

namespace StockFlow.Application.Transactions.Create.PolicyResolver;

internal interface ITransactionPolicyResolver
{
    TransactionType Type { get; }
    Task ValidateAsync(CreateTransactionCommand command, CancellationToken ct);
}
