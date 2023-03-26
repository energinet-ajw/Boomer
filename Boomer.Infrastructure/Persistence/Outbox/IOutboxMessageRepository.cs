namespace Boomer.Infrastructure.Persistence.Outbox;

public interface IOutboxMessageRepository
{
    Task AddAsync(OutboxMessage message);

    Task<IList<OutboxMessage>> GetByTakeAsync(int numberOfElements);

    void DeleteProcessedOlderThan(DateTime date);
}