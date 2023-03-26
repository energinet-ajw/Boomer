using Microsoft.EntityFrameworkCore;

namespace Boomer.Infrastructure.Persistence.Outbox;

public class OutboxMessageRepository : IOutboxMessageRepository
{
    private readonly IDatabaseContext _context;

    public OutboxMessageRepository(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task AddAsync(OutboxMessage message)
    {
        await _context.OutboxMessages.AddAsync(message).ConfigureAwait(false);
    }

    public async Task<IList<OutboxMessage>> GetByTakeAsync(int numberOfElements)
    {
        return await _context.OutboxMessages
            .Where(x => !x.ProcessedDate.HasValue)
            .OrderBy(x => x.CreationDate)
            .Take(numberOfElements)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public void DeleteProcessedOlderThan(DateTime date)
    {
        var messagesToDelete = _context.OutboxMessages.Where(x => x.CreationDate < date);
        _context.OutboxMessages.RemoveRange(messagesToDelete);
    }
}