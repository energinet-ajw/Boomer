using PetStore.Domain.Base;
using PetStore.Infrastructure.Persistence;

namespace PetStore.Infrastructure.EventDispatching;

public class DomainEventContainer : IDomainEventContainer
{
    private readonly IDatabaseContext _databaseContext;

    public DomainEventContainer(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public IReadOnlyCollection<IDomainEvent> GetAllDomainEventsAsync()
    {
       
        var domainEvents = _databaseContext.ChangeTracker
            .Entries<Entity>()
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        return domainEvents;
    }

    public void ClearAllDomainEvents()
    {
        _databaseContext.ChangeTracker
            .Entries<Entity>()
            .ToList()
            .ForEach(e => e.Entity.ClearDomainEvents());
    }
}