using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Internal;
using PetStore.Application.Base;
using PetStore.Infrastructure.Persistence.Outbox;

namespace PetStore.Infrastructure.EventDispatching.Integration;

public class IntegrationEventDispatcher : IIntegrationEventDispatcher
{
    private readonly IOutboxMessageRepository _outboxMessageRepository;
    private readonly ServiceBusSender _serviceBusSender;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISystemClock _systemClock;

    public IntegrationEventDispatcher(
        IOutboxMessageRepository outboxMessageRepository, 
        ServiceBusSender serviceBusSender, 
        IUnitOfWork unitOfWork,
        ISystemClock systemClock)
    {
        _outboxMessageRepository = outboxMessageRepository;
        _serviceBusSender = serviceBusSender;
        _unitOfWork = unitOfWork;
        _systemClock = systemClock;
    }
    
    public async Task DispatchAsync()
    {
        const int numberOfIntegrationEvents = 1000;
        var moreToDispatch = true;
        while (moreToDispatch)
        {
            moreToDispatch = await DispatchIntegrationEventsAsync(numberOfIntegrationEvents).ConfigureAwait(false);
            await _unitOfWork.CommitAsync().ConfigureAwait(false);
        }
    }
    
    private async Task<bool> DispatchIntegrationEventsAsync(int numberOfMessagesToDispatchInABulk)
    {
        // Fetch one more than bulk size to be able to test if there are more remaining
        var outboxMessages = await _outboxMessageRepository.GetByTakeAsync(numberOfMessagesToDispatchInABulk + 1).ConfigureAwait(false);
        if (!outboxMessages.Any()) return false;

        var batch = await CreateBatchWithinSizeLimitAsync(outboxMessages).ConfigureAwait(false);
        await _serviceBusSender.SendMessagesAsync(batch).ConfigureAwait(false);
        return outboxMessages.Count > numberOfMessagesToDispatchInABulk;
    }
    
    private async Task<ServiceBusMessageBatch> CreateBatchWithinSizeLimitAsync(IEnumerable<OutboxMessage> outboxMessages)
    {
        var batch = await _serviceBusSender.CreateMessageBatchAsync().ConfigureAwait(false);

        foreach (var outboxMessage in outboxMessages)
        {
            var serviceBusMessage = CreateServiceBusMessage(outboxMessage.Data, outboxMessage.MessageType);
            if (batch.TryAddMessage(serviceBusMessage))
            {
                outboxMessage.SetProcessed(_systemClock.UtcNow.UtcDateTime);
            }
            else
            {
                break;
            }
        }

        return batch;
    }
    
    private static ServiceBusMessage CreateServiceBusMessage(byte[] body, string subject)
    {
        var serviceBusMessage = new ServiceBusMessage
        {
            Body = new BinaryData(body),
            Subject = subject,
        };

        return serviceBusMessage;
    }
}