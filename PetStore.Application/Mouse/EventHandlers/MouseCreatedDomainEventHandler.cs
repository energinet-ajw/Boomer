using PetStore.Application.Base;
using PetStore.Application.Mouse.IntegrationEvents;
using PetStore.Domain.CatAggregate;
using PetStore.Domain.MouseAggregate;

namespace PetStore.Application.Mouse.EventHandlers;

public class MouseCreatedDomainEventHandler : IDomainEventHandler<MouseCreatedDomainEvent>
{
    private readonly IIntegrationEventPublisher _integrationEventPublisher;
    private readonly ICatRepository _catRepository;

    public MouseCreatedDomainEventHandler(IIntegrationEventPublisher integrationEventPublisher, ICatRepository catRepository)
    {
        _integrationEventPublisher = integrationEventPublisher;
        _catRepository = catRepository;
    }
    
    public async Task Handle(MouseCreatedDomainEvent domainEvent, CancellationToken token)
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