using PetStore.Domain.Base;

namespace PetStore.Domain.MouseAggregate;

public class Mouse : AggregateRoot
{
    public string Name { get; }

    public Mouse(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        AddDomainEvent(new MouseCreatedDomainEvent(Id));
    }

    // ReSharper disable once UnusedMember.Local
    // Used by Entity Framework.
    private Mouse()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        ClearDomainEvents();
    }

    public Guid Id { get; }
}