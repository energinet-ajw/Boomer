using PetStore.Infrastructure.Persistence.Outbox;

namespace PetStore.Infrastructure.Outbox;

public interface IOutboxMessageFactory
{
    OutboxMessage CreateFrom(byte[] data, string messageType);
}