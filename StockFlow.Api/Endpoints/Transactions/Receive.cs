using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.InventoryTransactions.ReceiveOrder;


namespace StockFlow.Api.Endpoints.Transactions;

internal sealed class Receive : IEndpoint
{
    private sealed record Request(List<ReceiveItemsRequest> Items);
    private sealed record ReceiveItemsRequest(
        Guid ProductId,
        int QuantityChange,
        Guid OrderId,
        Guid WarehouseId,
        decimal? UnitCost,
        string? Reason);


    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("transactions/receive", async (
            Request request,
            ICommandHandler<ReceiveOrderCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new ReceiveOrderCommand([..request.Items.Select(i => new ReceiveItems(
                i.ProductId,
                i.QuantityChange,
                i.OrderId,
                i.WarehouseId))]);

            Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
            .WithTags(Tags.Transactions);
    }
}
