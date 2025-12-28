using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Domain.Entities.Transfers;

public static class TransferErrors
{
    public static readonly Error NotFound = new(
           "Transfers.Found",
           "The item with the specified identifier was not found");

    public static readonly Error AlreadyDispatch = new(
         "Transfers.TransferAlreadyDispatched",
         "The item with the specified identifier was already In-Transit");

    public static readonly Error InsufficientStock = new(
          "Transfers.InsufficientStock",
          "Insufficient stock for this product'");
}
