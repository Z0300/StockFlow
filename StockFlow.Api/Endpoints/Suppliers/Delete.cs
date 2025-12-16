
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Suppliers.Delete;

namespace StockFlow.Api.Endpoints.Suppliers;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("suppliers/{id:guid}", async (
               Guid id,
               ICommandHandler<DeleteSupplierCommand> handler,
               CancellationToken cancellationToken) =>
        {
            var command = new DeleteSupplierCommand(id);
            Result result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.NoContent, CustomResults.Problem);
        })
           .WithTags(Tags.Suppliers);
    }
}
