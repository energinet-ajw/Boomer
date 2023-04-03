using PetStore.Domain.Base;

namespace PetStore.Domain.MouseAggregate;

public class Mouse : AggregateRoot
{
    public Mouse()
    {
        Id = Guid.NewGuid();
        AddDomainEvent(new MouseCreatedDomainEvent(Id));
    }

    public Guid Id { get; }
}