using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Enums;
using StockFlow.Domain.OrderItems;
using StockFlow.Domain.Orders;

namespace StockFlow.Application.Orders.Create;

internal sealed class CreateOrderCommandHandler(IApplicationDbContext context)
    : ICommandHandler<CreateOrderCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            WarehouseId = command.WarehouseId,
            SupplierId = command.SupplierId,
            OrderStatus = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            OrderItems = [.. command.Items
                .Select(item => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                })]
        };

        context.Orders.Add(order);
        await context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
