using SharedKernel;
using StockFlow.Domain.Users;

namespace StockFlow.Application.Users.Register;

internal sealed class UserRegisteredDomainEventHandler : IDomainEventHandler<UserRegisteredDomainEvent>
{
    public Task HandleAsync(UserRegisteredDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        
        return Task.CompletedTask;
    }
}

