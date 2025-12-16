
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Products.Update;

namespace StockFlow.Api.Endpoints.Products;

public class Update : IEndpoint
{
    public sealed record Request(Guid Id, string Name, string Sku, decimal Price, Guid CategoryId, Guid WarehouseId);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("products/{id:guid}", async (
                Guid id,
                Request request,
                ICommandHandler<UpdateProductCommand> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new UpdateProductCommand(
                    id,
                    request.Name,
                    request.Sku,
                    request.Price,
                    request.CategoryId,
                    request.WarehouseId);
                Result result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .WithTags(Tags.Products);
    }
}
