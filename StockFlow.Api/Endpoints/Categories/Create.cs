
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Categories.Create;

namespace StockFlow.Api.Endpoints.Categories;

internal sealed class Create : IEndpoint
{
    public sealed record Request(string Name, string Description);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("categories", async (
                Request request,
                ICommandHandler<CreateCategoryCommand, Guid> handler,
                CancellationToken cancellationToken) =>
            {
                var command = new CreateCategoryCommand(request.Name, request.Description);
                Result<Guid> result = await handler.Handle(command, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Categories);
    }
}
