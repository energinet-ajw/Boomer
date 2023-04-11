using PetStore.Domain.Base;

namespace PetStore.Domain.MouseAggregate;

public class MouseCreatedDomainEvent : IDomainEvent
{
    public readonly Guid MouseId;

    public MouseCreatedDomainEvent(Guid mouseId)
    {
        MouseId = mouseId;
    }
}