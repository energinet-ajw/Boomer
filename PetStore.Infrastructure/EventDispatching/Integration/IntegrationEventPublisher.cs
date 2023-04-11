using System.Text;
using PetStore.Application.Base;
using PetStore.Infrastructure.Outbox;
using PetStore.Infrastructure.Persistence.Outbox;

namespace PetStore.Infrastructure.EventDispatching.Integration;

public class IntegrationEventPublisher : IIntegrationEventPublisher
{
    private readonly IOutboxMessageRepository _outboxMessageRepository;
    private readonly IOutboxMessageFactory _outboxMessageFactory;

    public IntegrationEventPublisher(IOutboxMessageRepository outboxMessageRepository, IOutboxMessageFactory outboxMessageFactory)
    {
        _outboxMessageRepository = outboxMessageRepository;
        _outboxMessageFactory = outboxMessageFactory;
    }
    
    public async Task PublishAsync(IIntegrationEvent integrationEvent)
    {
        var data = Encoding.ASCII.GetBytes(integrationEvent.ToString() ?? string.Empty);
        var messageType = integrationEvent.GetType().ToString();
        var outboxMessage = _outboxMessageFactory.CreateFrom(data, messageType);
        await _outboxMessageRepository.AddAsync(outboxMessage).ConfigureAwait(false);
    }
}