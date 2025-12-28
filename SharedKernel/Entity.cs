
namespace SharedKernel;

public abstract class Entity<TEntityId> : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];
    protected Entity(TEntityId id)
    {
        Id = id;
    }

    protected Entity() { }

    public TEntityId Id { get; init; }
    public void ClearDomainEvents() => _domainEvents.Clear();
    public IReadOnlyList<IDomainEvent> GetDomainEvents() => [.. _domainEvents];
    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
