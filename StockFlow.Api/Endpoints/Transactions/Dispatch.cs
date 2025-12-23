
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Transactions.DispatchTransfer;

namespace StockFlow.Api.Endpoints.Transactions;

internal sealed class Dispatch : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("transactions/{transferId:guid}/dispatch", async (
            Guid transferId,
            ICommandHandler<DispatchTransferCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new DispatchTransferCommand(transferId);
            Result result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.NoContent, CustomResults.Problem);
        })
            .WithTags(Tags.Transactions);
    }
}
