using SharedKernel;

namespace StockFlow.Domain.DomainErrors;

public static class TransferErrors
{
    public static Error TransferNotFound(Guid TransferId) => Error.NotFound(
           "Transfers.NotFound",
           $"The transfer item with Id = '{TransferId}' was not found.");

    public static Error TransferAlreadyDispatched(Guid TransferId) => Error.NotFound(
         "Transfers.TransferAlreadyDispatched",
         $"The transfer item with Id = '{TransferId}' was not found.");

    public static Error ProductNotFound => Error.NotFound(
           "Transfers.ProductNotFound",
           "One or more products do not exist.");

    public static Error InsufficientStock(Guid productId) => Error.Failure(
          "Transfers.InsufficientStock",
          $"Insufficient stock for product '{productId}'");
}
