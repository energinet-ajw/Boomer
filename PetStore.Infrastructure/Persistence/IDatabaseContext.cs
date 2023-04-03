using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PetStore.Infrastructure.Persistence.Outbox;

namespace PetStore.Infrastructure.Persistence;

public interface IDatabaseContext
{
    DbSet<OutboxMessage> OutboxMessages { get; }

    DbSet<Domain.MouseAggregate.Mouse> Mice { get; }
    ChangeTracker ChangeTracker { get; }

    Task<int> SaveChangesAsync(CancellationToken token = default);
}