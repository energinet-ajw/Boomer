using PetStore.Domain.Base;

namespace PetStore.Infrastructure.EventDispatching;

public interface IDomainEventContainer
{
    Task<IReadOnlyCollection<IDomainEvent>> GetAllDomainEventsAsync();
    Task ClearAllDomainEvents();
}