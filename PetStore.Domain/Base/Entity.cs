namespace PetStore.Domain.Base;

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
        Console.Out.WriteLine(domainEvent.GetType().Name + " added to domain events.");
        _domainEvents.Add(domainEvent);
    }
}