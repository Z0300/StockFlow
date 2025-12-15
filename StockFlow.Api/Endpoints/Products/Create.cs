
using StockFlow.Application.Products.Create;

namespace StockFlow.Api.Endpoints.Products;

internal sealed class Create : IEndpoint
{
    public sealed record Request(string Name, string Sku, decimal Price, Guid CategoryId, Guid WarehouseId);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (
                Request request,
                ICommandHandler<CreateProductCommand, Guid> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new CreateProductCommand(
                    request.Name,
                    request.Sku,
                    request.Price,
                    request.CategoryId,
                    request.WarehouseId);
                Result<Guid> result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Products);
    }
}
