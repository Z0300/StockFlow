
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Categories.Delete;

namespace StockFlow.Api.Endpoints.Categories;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("categories/{categoryId:guid}", async (
                Guid categoryId,
                ICommandHandler<DeleteCategoryCommand> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new DeleteCategoryCommand(categoryId);
                Result result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .WithTags(Tags.Categories);
    }
}
