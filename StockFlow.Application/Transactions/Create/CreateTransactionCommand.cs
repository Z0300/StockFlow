using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Enums;

namespace StockFlow.Application.Transactions.Create;

public sealed record CreateTransactionCommand(
    Guid WarehouseId,
    Guid? OrderId,
    TransactionType TransactionType,
    string? Reason,
    List<TransactionItems> Items) : ICommand<Guid>;

public sealed record TransactionItems(
    Guid ProductId,
    int QuantityChange,
    decimal UnitCost
);

