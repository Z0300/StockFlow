
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Transactions.TransferOut;

namespace StockFlow.Api.Endpoints.Transactions;

internal sealed class Transfer : IEndpoint
{
    private sealed record Request(
       Guid SourceWarehouseId,
        Guid DestinationWarehouseId,
        List<TransferOutRequestItems> Items);

    private sealed record TransferOutRequestItems(
        Guid ProductId,
        int RequestedQuantity);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("transactions/transfer", async (
            Request request,
            ICommandHandler<CreateTransferOutCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateTransferOutCommand(
                request.SourceWarehouseId,
                request.DestinationWarehouseId,
                [.. request.Items.Select(i => new TransferOutItems(
                    i.ProductId,
                    i.RequestedQuantity))]);

            Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
             .WithTags(Tags.Transactions);
    }
}
