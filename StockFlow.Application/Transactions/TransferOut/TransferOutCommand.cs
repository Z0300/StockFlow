using System;
using System.Collections.Generic;
using System.Text;
using StockFlow.Application.Abstractions.Messaging;
using StockFlow.Domain.Enums;

namespace StockFlow.Application.Transactions.TransferOut;

public sealed record TransferOutCommand(
    Guid SourceWarehouseId,
    Guid DestinationWarehouseId,
    List<TransferOutItems> Items) : ICommand<Guid>;

public sealed record TransferOutItems(
    Guid ProductId,
    int RequestedQuantity
);

