
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Categories.Create;
using StockFlow.Application.Suppliers.Create;

namespace StockFlow.Api.Endpoints.Suppliers;

internal sealed class Create : IEndpoint
{
    public sealed record Request(string Name, string ContactInfo);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("suppliers", async (
               Request request,
               ICommandHandler<CreateSupplierCommand, Guid> handler,
               CancellationToken cancellationToken) =>
        {
            var command = new CreateSupplierCommand(request.Name, request.ContactInfo);
            Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
           .WithTags(Tags.Suppliers);
    }
}
