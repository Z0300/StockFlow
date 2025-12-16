
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Products.Delete;

namespace StockFlow.Api.Endpoints.Products;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{id:guid}", async (
                Guid id,
                ICommandHandler<DeleteProductCommand> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteProductCommand(id);
                Result result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .WithTags(Tags.Products);
    }
}
