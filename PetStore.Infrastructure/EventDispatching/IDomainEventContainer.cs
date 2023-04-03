using PetStore.Domain.Base;

namespace PetStore.Infrastructure.EventDispatching;

public interface IDomainEventContainer
{
    IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();
    void ClearAllDomainEvents();
}