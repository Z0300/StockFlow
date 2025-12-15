using StockFlow.Application.Products.Create;
using StockFlow.Application.Warehouses.Create;

namespace StockFlow.Api.Endpoints.Warehouses;

internal sealed class Create : IEndpoint
{
    public sealed record Request(string Name, string Location);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/warehouses", async (
            Request request,
            ICommandHandler<CreateWarehouseCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateWarehouseCommand(
                    request.Name,
                    request.Location);

            Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
            .WithTags(Tags.Warehouses);
    }
}
