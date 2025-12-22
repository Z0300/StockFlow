using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel;

namespace StockFlow.Domain.DomainErrors;

public static class TransferErrors
{
    public static Error ProductNotFound => Error.NotFound(
           "Transfers.NotFound",
           $"One or more products do not exist.");

    public static Error InsufficientStock(Guid productId) => Error.Conflict(
          "Transfers.NotFound",
          $"Insufficient stock for product {productId}");
}
