
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Products.GetById;

namespace StockFlow.Api.Endpoints.Products;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id:guid}", async (
                Guid id,
                IQueryHandler<GetProductByIdQuery, GetProductByIdResponse> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetProductByIdQuery(id);
                Result<GetProductByIdResponse> result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Products);
    }
}
