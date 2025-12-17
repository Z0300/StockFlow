
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Orders.Cancel;

namespace StockFlow.Api.Endpoints.Orders;

internal sealed class Cancel : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("orders/{orderId:guid}/cancel", async (
                 Guid orderId,
                 ICommandHandler<CancelOrderCommand> handler,
                 CancellationToken cancellationToken) =>
        {
            var command = new CancelOrderCommand(orderId);
            Result result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.NoContent, CustomResults.Problem);
        })
             .WithTags(Tags.Orders);
    }
}
