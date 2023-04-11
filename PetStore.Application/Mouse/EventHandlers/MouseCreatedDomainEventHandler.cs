using PetStore.Application.Base;
using PetStore.Application.Mouse.IntegrationEvents;
using PetStore.Domain.MouseAggregate;

namespace PetStore.Application.Mouse.EventHandlers;

public class MouseCreatedDomainEventHandler : IDomainEventHandler<MouseCreatedDomainEvent>
{
    private readonly IIntegrationEventPublisher _integrationEventPublisher;

    public MouseCreatedDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher)
    {
        _integrationEventPublisher = integrationEventPublisher;
    }
    
    public async Task Handle(MouseCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await Console.Out.WriteLineAsync($"MouseCreatedEventHandler: New mouse created (Id: {domainEvent.MouseId }).").ConfigureAwait(false);
        var integrationEvent = new MouseCreatedIntegrationEvent
        {
            Id = Guid.NewGuid(),
            Data = domainEvent.MouseId.ToByteArray()
        };
        await _integrationEventPublisher.PublishAsync(integrationEvent).ConfigureAwait(false);
    }
}