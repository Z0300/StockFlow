using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Warehouses.Update;

namespace StockFlow.Api.Endpoints.Warehouses;

internal sealed class Update : IEndpoint
{
    public sealed record Request(Guid Id, string Name, string Location);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/warehouses/{id:guid}", async (
                Guid id,
                Request request,
                ICommandHandler<UpdateWarehouseCommand> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new UpdateWarehouseCommand(id, request.Name, request.Location);
                Result result = await handler.Handle(command, cancellationToken);
                return result.Match(() => Results.NoContent(), CustomResults.Problem);
            })
            .WithTags(Tags.Warehouses);
    }
}
