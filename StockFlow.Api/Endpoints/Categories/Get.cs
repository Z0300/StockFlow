
using StockFlow.Application.Categories.Get;
using StockFlow.Application.Categories.Shared;

namespace StockFlow.Api.Endpoints.Categories;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("categories", async (
                IQueryHandler<GetCategoryQuery, List<CategoryResponse>> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetCategoryQuery();
                Result<List<CategoryResponse>> result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Categories);
    }
}
