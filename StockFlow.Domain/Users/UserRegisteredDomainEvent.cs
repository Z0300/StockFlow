using SharedKernel;

namespace StockFlow.Domain.Users;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;