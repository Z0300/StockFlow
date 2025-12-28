using StockFlow.Application.Orders.Create;

namespace StockFlow.Api.Endpoints.Orders;

public sealed record CreateOrderRequest
{
    public required Guid WarehouseId { get; init; }
    public required Guid SupplierId { get; init; }
    public required List<AppOrderItems> Items { get; init; }
}

