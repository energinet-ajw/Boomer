using PetStore.Domain.Base;
using PetStore.Domain.MouseAggregate;

namespace PetStore.Domain.CatAggregate;

public class Cat : AggregateRoot
{
    public string Name { get; }

    public Cat(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        AddDomainEvent(new CatCreatedDomainEvent(Id));
    }

    // ReSharper disable once UnusedMember.Local
    // Used by Entity Framework.
    private Cat()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        ClearDomainEvents();
    }
    public Guid Id { get; }
}