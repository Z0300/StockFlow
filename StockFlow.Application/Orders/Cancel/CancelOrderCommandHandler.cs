using Microsoft.EntityFrameworkCore;
using SharedKernel;
using StockFlow.Application.Abstractions.Data;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Enums;
using StockFlow.Domain.Orders;

namespace StockFlow.Application.Orders.Cancel;

internal sealed class CancelOrderCommandHandler(IApplicationDbContext context)
    : ICommandHandler<CancelOrderCommand>
{
    public async Task<Result> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        Order? order = await context.Orders
            .SingleOrDefaultAsync(x => x.Id == command.OrderId, cancellationToken);

        if (order is null)
        {
            return Result.Failure(OrderErrors.NotFound(command.OrderId));
        }

        order.OrderStatus = OrderStatus.Cancelled;
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
