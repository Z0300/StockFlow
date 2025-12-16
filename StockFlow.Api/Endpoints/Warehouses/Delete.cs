using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Warehouses.Delete;

namespace StockFlow.Api.Endpoints.Warehouses;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/warehouses/{id:guid}", async (
                Guid id,
                ICommandHandler<DeleteWarehouseCommand> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteWarehouseCommand(id);
                Result result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .WithTags(Tags.Warehouses);
    }
}
