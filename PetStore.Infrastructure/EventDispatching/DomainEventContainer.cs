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

    public async Task<IReadOnlyCollection<IDomainEvent>> GetAllDomainEventsAsync()
    {
       
        var domainEvents = _databaseContext.ChangeTracker
            .Entries<Entity>()
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        await Console.Out.WriteLineAsync("Getting all domain events from cache...");
        return domainEvents;
    }

    public async Task ClearAllDomainEvents()
    {
        await Console.Out.WriteLineAsync("Clearing all domain events from cache...");
        _databaseContext.ChangeTracker
            .Entries<Entity>()
            .ToList()
            .ForEach(e => e.Entity.ClearDomainEvents());
    }
}