
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Categories.Update;

namespace StockFlow.Api.Endpoints.Categories;

internal sealed class Update : IEndpoint
{
    public sealed record Request(Guid Id, string Name, string Description);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("categories/{categoryId:guid}", async (
                Guid categoryId,
                Request request,
                ICommandHandler<UpdateCategoryCommand> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new UpdateCategoryCommand(
                    categoryId,
                    request.Name,
                    request.Description);
                Result result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .WithTags(Tags.Categories);
    }
}
