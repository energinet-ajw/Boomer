using PetStore.Domain.Base;

namespace PetStore.Domain.MouseAggregate;

public class MouseCreatedDomainEvent : IDomainEvent
{
    public readonly Guid Id;

    public MouseCreatedDomainEvent(Guid id)
    {
        Id = id;
    }
}