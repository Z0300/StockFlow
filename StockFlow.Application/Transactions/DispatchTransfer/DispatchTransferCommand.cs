using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Transactions.DispatchTransfer;

public sealed record DispatchTransferCommand(Guid TransferId) : ICommand;
