using StockFlow.Domain.Entities.Abstractions;

namespace StockFlow.Domain.Entities.Transfers.Events;

public sealed record TransferDispatchedEvent(TransferId TransferId) : IDomainEvent;
