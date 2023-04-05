using PetStore.Application.Base;
using PetStore.Domain.MouseAggregate;

namespace PetStore.Application.Mouse.EventHandlers;

public class MouseCreatedDomainEventHandler : IDomainEventHandler<MouseCreatedDomainEvent>
{
    public async Task Handle(MouseCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await Console.Out.WriteLineAsync($"MouseCreatedEventHandler: New mouse created (Id: {notification.Id }).").ConfigureAwait(false);
    }
}