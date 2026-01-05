using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Domain.Entities.Transactions;

public static class TransactionErrors
{
    public static readonly Error NotFound = new(
        "Transaction.NotFound",
        "The transaction item with the specified identifier was not found");

    public static readonly Error TransferNotFound = new(
       "Transfer.NotFound",
       "The transfer item with the specified identifier was not found");
}
