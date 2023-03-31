using Boomer.Domain;

namespace PetStore.Domain.MouseAggregate;

public class Mouse : AggregateRoot
{
    public Mouse()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
}

public abstract class AggregateRoot : Entity
{
}

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}