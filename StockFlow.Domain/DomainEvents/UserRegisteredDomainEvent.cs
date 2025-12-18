using SharedKernel;

namespace StockFlow.Domain.DomainEvents;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;