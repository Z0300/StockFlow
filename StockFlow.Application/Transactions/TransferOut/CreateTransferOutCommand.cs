using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Transactions.TransferOut;

public sealed record CreateTransferOutCommand(
    Guid SourceWarehouseId,
    Guid DestinationWarehouseId,
    List<TransferOutItems> Items) : ICommand<Guid>;

public sealed record TransferOutItems(
    Guid ProductId,
    int RequestedQuantity
);

