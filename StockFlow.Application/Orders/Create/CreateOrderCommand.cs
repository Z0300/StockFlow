using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Orders.Create;

public sealed record CreateOrderCommand(
    Guid WarehouseId,
    Guid SupplierId,
    List<AppOrderItems> Items
    ) : ICommand<Guid>;

public sealed record AppOrderItems(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice);

