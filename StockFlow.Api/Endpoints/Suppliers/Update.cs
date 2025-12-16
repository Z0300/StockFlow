
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Suppliers.Update;

namespace StockFlow.Api.Endpoints.Suppliers;

public class Update : IEndpoint
{
    public sealed record Request(Guid SupplierId, string Name, string ContactInfo);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("suppliers/{id:guid}", async (
               Guid id,
               Request request,
               ICommandHandler<UpdateSupplierCommand> handler,
               CancellationToken cancellationToken) =>
        {
            var command = new UpdateSupplierCommand(id, request.Name, request.ContactInfo);
            Result result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.NoContent, CustomResults.Problem);
        })
           .WithTags(Tags.Suppliers);
    }
}
