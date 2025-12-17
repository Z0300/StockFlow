
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Suppliers.Get;
using StockFlow.Application.Suppliers.Shared;

namespace StockFlow.Api.Endpoints.Suppliers;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("suppliers", async (
               IQueryHandler<GetSupplierQuery, List<SupplierResponse>> handler,
               CancellationToken cancellationToken) =>
        {
            var query = new GetSupplierQuery();
            Result<List<SupplierResponse>> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
           .WithTags(Tags.Suppliers);
    }
}
