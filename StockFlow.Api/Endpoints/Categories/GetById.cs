
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Categories.GetById;
using StockFlow.Application.Categories.Shared;

namespace StockFlow.Api.Endpoints.Categories;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("categories/{categoryId:guid}", async (
                Guid categoryId,
                IQueryHandler<GetCategoryByIdQuery, CategoryResponse> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetCategoryByIdQuery(categoryId);
                Result<CategoryResponse> result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Categories);
    }
}
