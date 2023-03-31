using Boomer.Infrastructure.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using PetStore.Infrastructure.Persistence.Outbox;

namespace PetStore.Infrastructure;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    // Added to support Moq in tests
    public DatabaseContext()
    {
    }

    public DbSet<OutboxMessage> OutboxMessages { get; } = null!;

    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OutboxMessageEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}