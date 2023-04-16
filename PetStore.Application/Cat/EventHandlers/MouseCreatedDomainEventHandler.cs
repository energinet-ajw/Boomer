using PetStore.Application.Base;
using PetStore.Domain.CatAggregate;

namespace PetStore.Application.Cat.EventHandlers;

public class CatCreatedDomainEventHandler : IDomainEventHandler<CatCreatedDomainEvent>
{
   
    public async Task Handle(CatCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await Console.Out.WriteLineAsync($"CatCreatedDomainEventHandler: New cat created (Id: {domainEvent.CatId}).").ConfigureAwait(false);
    }
}