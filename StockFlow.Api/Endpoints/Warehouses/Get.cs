using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Warehouses.Get;
using StockFlow.Application.Warehouses.Shared;

namespace StockFlow.Api.Endpoints.Warehouses;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/warehouses", async (
                IQueryHandler<GetWarehouseQuery, List<WarehouseResponse>> handler,
                CancellationToken cancellationToken) =>
            {
                var query = new GetWarehouseQuery();
                Result<List<WarehouseResponse>> result = await handler.Handle(query, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .WithTags(Tags.Warehouses);
    }
}
