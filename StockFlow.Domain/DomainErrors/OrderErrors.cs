using SharedKernel;

namespace StockFlow.Domain.DomainErrors;

public static class OrderErrors
{
    public static Error NotFound(Guid orderId) => Error.NotFound(
            "order.NotFound",
            $"Order with ID '{orderId}' was not found.");
}
