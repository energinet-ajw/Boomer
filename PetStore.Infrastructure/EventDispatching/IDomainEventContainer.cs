using PetStore.Domain.Base;

namespace PetStore.Infrastructure.EventDispatching;

public interface IDomainEventContainer
{
    IReadOnlyCollection<IDomainEvent> GetAllDomainEventsAsync();
    
    void ClearAllDomainEvents();
}