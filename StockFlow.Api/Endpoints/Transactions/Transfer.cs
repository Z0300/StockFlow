
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Transactions.TransferOut;
using StockFlow.Domain.Enums;

namespace StockFlow.Api.Endpoints.Transactions;

internal sealed class Transfer : IEndpoint
{
    private sealed record Request(
       Guid SourceWarehouseId,
        Guid DestinationWarehouseId,
        TransferStatus Status,
        List<TransferOutRequestItems> Items);

    private sealed record TransferOutRequestItems(
        Guid ProductId,
        int RequestedQuantity,
        int ReceivedQuantity);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("transactions/transferOut", async (
            Request request,
            ICommandHandler<TransferOutCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new TransferOutCommand(
                request.SourceWarehouseId,
                request.DestinationWarehouseId,
                request.Status,
                [.. request.Items.Select(i => new TransferOutItems(
                    i.ProductId,
                    i.RequestedQuantity,
                    i.ReceivedQuantity))]);

            Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
             .WithTags(Tags.Transactions);
    }
}
