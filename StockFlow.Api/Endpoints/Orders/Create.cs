
using SharedKernel;
using StockFlow.Api.Extensions;
using StockFlow.Api.Infrastructure;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Application.Orders.Create;

namespace StockFlow.Api.Endpoints.Orders;

internal sealed class Create : IEndpoint
{
    public sealed record Request(Guid WarehouseId, Guid SupplierId, List<OrderItems> Items);
    public sealed record OrderItems(Guid ProductId, int Quantity, decimal UnitPrice);
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("orders", async (
                Request request,
                ICommandHandler<CreateOrderCommand, Guid> handler,
                CancellationToken cancellationToken) =>
        {
            var command = new CreateOrderCommand(
                request.WarehouseId,
                request.SupplierId,
                [.. request.Items.Select(i => new AppOrderItems(i.ProductId, i.Quantity, i.UnitPrice))]);

            Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
            .WithTags(Tags.Orders);
    }
}
