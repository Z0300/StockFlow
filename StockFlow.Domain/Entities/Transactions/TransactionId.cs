using System;
using System.Collections.Generic;
using System.Text;

namespace StockFlow.Domain.Entities.Transactions;

public record TransactionId(Guid Value)
{
    public static TransactionId New() => new(Guid.NewGuid());
}

