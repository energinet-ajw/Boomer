using Microsoft.Extensions.Internal;
using PetStore.Infrastructure.Persistence.Outbox;

namespace PetStore.Infrastructure.Outbox;

public class OutboxMessageFactory : IOutboxMessageFactory
{
    private readonly ISystemClock _systemClock;

    public OutboxMessageFactory(ISystemClock systemClock)
    {
        _systemClock = systemClock;
    }
    
    public OutboxMessage CreateFrom(byte[] data, string messageType)
    {
        return new OutboxMessage(data, messageType, _systemClock.UtcNow.UtcDateTime);
    }
}