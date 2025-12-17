
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Orders.Get;

namespace StockFlow.Api.Endpoints.Orders;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("orders", async (
                IQueryHandler<GetOrderQuery, List<GetOrderResponse>> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetOrderQuery();
                Result<List<GetOrderResponse>> result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Orders);
    }
}
