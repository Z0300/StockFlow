
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Suppliers.GetById;
using StockFlow.Application.Suppliers.Shared;

namespace StockFlow.Api.Endpoints.Suppliers;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("suppliers/{id:guid}", async (
               Guid id,
               IQueryHandler<GetSupplierByIdQuery, SupplierResponse> handler,
               CancellationToken cancellationToken) =>
        {
            var query = new GetSupplierByIdQuery(id);
            Result<SupplierResponse> result = await handler.Handle(query, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
           .WithTags(Tags.Suppliers);
    }
}
