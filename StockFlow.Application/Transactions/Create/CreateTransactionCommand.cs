using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Enums;

namespace StockFlow.Application.Transactions.Create;

public sealed record CreateTransactionCommand(List<ReceiveItems> Items) : ICommand<Guid>;

public sealed record ReceiveItems(
    Guid ProductId,
    Guid WarehouseId,
    TransactionType TransactionType,
    int QuantityChange,
    decimal UnitCost,
    Guid? OrderId,
    string? Reason
);

