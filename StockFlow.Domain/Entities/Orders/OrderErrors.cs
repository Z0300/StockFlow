using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Domain.Entities.Orders;

public static class OrderErrors
{
    public static readonly Error NotFound = new(
            "Order.Found",
            "The order with the specified identifier was not found");

    public static readonly Error InvalidOperation = new(
            "Order.InvalidOperation",
            "Invalid operation for the current resource state.");
}
