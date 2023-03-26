using Microsoft.EntityFrameworkCore;

namespace Boomer.Infrastructure.Persistence.Outbox;

public interface IDatabaseContext
{
    DbSet<OutboxMessage> OutboxMessages { get; }

    Task<int> SaveChangesAsync();
}