using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Transactions.Create;
using StockFlow.Domain.Enums;

namespace StockFlow.Api.Endpoints.Transactions;

internal sealed class Create : IEndpoint
{
    private sealed record Request(
        Guid WarehouseId,
        Guid? OrderId,
        TransactionType TransactionType,
        string? Reason,
        List<RequestItems> Items);

    private sealed record RequestItems(
        Guid ProductId,
        int QuantityChange,
        decimal UnitCost);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("transactions", async (
            Request request,
            ICommandHandler<CreateTransactionCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateTransactionCommand(
                request.WarehouseId,
                request.OrderId,
                request.TransactionType,
                request.Reason, 
                [..request.Items.Select(i => new TransactionItems(
                    i.ProductId,
                    i.QuantityChange,
                    i.UnitCost))]);

            Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
            .WithTags(Tags.Transactions);
    }
}
