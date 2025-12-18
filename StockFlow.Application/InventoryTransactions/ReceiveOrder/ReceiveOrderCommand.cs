using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.InventoryTransactions.ReceiveOrder;

public sealed record ReceiveOrderCommand(List<ReceiveItems> Items) : ICommand<Guid>;

public sealed record ReceiveItems(
    Guid ProductId,
    int QuantityChange,
    Guid OrderId,
    Guid WarehouseId);

