using MediatR;
using PetStore.Domain.MouseAggregate;

namespace PetStore.Application.Mouse.Listeners;

public class MouseCreatedEventListener : INotificationHandler<MouseCreatedDomainEvent>
{
    public async Task Handle(MouseCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await Console.Out.WriteLineAsync($"MouseCreatedEventListener: New mouse created (Id: {notification.Id }).").ConfigureAwait(false);
    }
}