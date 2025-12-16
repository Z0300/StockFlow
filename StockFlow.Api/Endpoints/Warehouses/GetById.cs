using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Warehouses.GetById;
using StockFlow.Application.Warehouses.Shared;

namespace StockFlow.Api.Endpoints.Warehouses;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/warehouses/{id:guid}", async (
                Guid id,
                IQueryHandler<GetWarehouseByIdQuery, WarehouseResponse> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetWarehouseByIdQuery(id);
                Result<WarehouseResponse> result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Warehouses);
    }
}
