
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Products.Get;

namespace StockFlow.Api.Endpoints.Products;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (
                IQueryHandler<GetProductQuery, List<GetProductResponse>> handler,
                CancellationToken cancellationToken) =>
        {
            var query = new GetProductQuery();
            Result<List<GetProductResponse>> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
            .WithTags(Tags.Products);
    }
}
