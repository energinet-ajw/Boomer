using Microsoft.EntityFrameworkCore;
using PetStore.Infrastructure.Persistence.Mouse;
using PetStore.Infrastructure.Persistence.Outbox;

namespace PetStore.Infrastructure.Persistence;

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

    public DbSet<OutboxMessage> OutboxMessages { get; private set; } = null!;
    public DbSet<Domain.MouseAggregate.Mouse> Mice { get; private set; } = null!;

    public new Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return base.SaveChangesAsync(token);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("PetStoreDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OutboxMessageEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MouseEntityConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}