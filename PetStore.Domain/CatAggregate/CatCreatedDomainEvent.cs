using PetStore.Domain.Base;

namespace PetStore.Domain.CatAggregate;

public class CatCreatedDomainEvent : IDomainEvent
{
    public readonly Guid CatId;

    public CatCreatedDomainEvent(Guid catId)
    {
        CatId = catId;
    }
}