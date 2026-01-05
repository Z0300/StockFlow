using StockFlow.Application.Abstractions.Messaging;

namespace StockFlow.Application.Transactions.GetTransfer;

public sealed record GetTransferQuery : IQuery<IReadOnlyCollection<TransfersResponse>>;
