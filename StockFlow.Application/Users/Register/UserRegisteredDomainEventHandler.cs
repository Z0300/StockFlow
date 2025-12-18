using SharedKernel;
using StockFlow.Domain.DomainEvents;

namespace StockFlow.Application.Users.Register;

internal sealed class UserRegisteredDomainEventHandler : IDomainEventHandler<UserRegisteredDomainEvent>
{
    public Task HandleAsync(UserRegisteredDomainEvent domainEvent, CancellationToken cancellationToken)
    {

        return Task.CompletedTask;
    }
}

