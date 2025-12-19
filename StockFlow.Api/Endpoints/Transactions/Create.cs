using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Transactions.Create;
using StockFlow.Domain.Enums;

namespace StockFlow.Api.Endpoints.Transactions;

internal sealed class Create : IEndpoint
{
    private sealed record Request(List<ReceiveItemsRequest> Items);
    private sealed record ReceiveItemsRequest(
        Guid ProductId,
        Guid WarehouseId,
        TransactionType TransactionType,
        int QuantityChange,
        decimal UnitCost,
        Guid? OrderId,
        string? Reason);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("transactions", async (
            Request request,
            ICommandHandler<CreateTransactionCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateTransactionCommand([..request.Items.Select(i => new ReceiveItems(
                i.ProductId,
                i.WarehouseId,
                i.TransactionType,
                i.QuantityChange,
                i.UnitCost,
                i.OrderId,
                i.Reason))]);

            Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
            .WithTags(Tags.Transactions);
    }
}
