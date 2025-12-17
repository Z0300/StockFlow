
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Orders.GetById;

namespace StockFlow.Api.Endpoints.Orders;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("orders/{orderId:guid}", async (
                Guid orderId,
                IQueryHandler<GetOrderByIdQuery, GetOrderByIdResponse> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetOrderByIdQuery(orderId);
                Result<GetOrderByIdResponse> result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Orders);
    }
}
