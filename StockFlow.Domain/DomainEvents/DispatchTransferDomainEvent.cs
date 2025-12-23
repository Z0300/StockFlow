using SharedKernel;

namespace StockFlow.Domain.DomainEvents;

public sealed record DispatchTransferDomainEvent(Guid TransferId) : IDomainEvent;

