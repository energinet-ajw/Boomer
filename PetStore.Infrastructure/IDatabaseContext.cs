using Microsoft.EntityFrameworkCore;
using PetStore.Infrastructure.Persistence.Outbox;

namespace Boomer.Infrastructure.Persistence.Outbox;

public interface IDatabaseContext
{
    DbSet<OutboxMessage> OutboxMessages { get; }

    Task<int> SaveChangesAsync();
}