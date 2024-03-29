using Microsoft.EntityFrameworkCore;
using PetStore.Infrastructure.Persistence.Cat;
using PetStore.Infrastructure.Persistence.Mouse;
using PetStore.Infrastructure.Persistence.Outbox;

namespace PetStore.Infrastructure.Persistence;

public sealed class PetStoreDatabaseContext : DbContext, IDatabaseContext
{
    public PetStoreDatabaseContext(DbContextOptions<PetStoreDatabaseContext> options)
        : base(options)
    {
        // TODO - Removed when using real database
        Database.EnsureCreated();
    }

    // Added to support Moq in tests
    public PetStoreDatabaseContext()
    {
    }

    public DbSet<OutboxMessage> OutboxMessages { get; private set; } = null!;
    public DbSet<Domain.MouseAggregate.Mouse> Mice { get; private set; } = null!;

    public DbSet<Domain.CatAggregate.Cat> Cats { get; private set; } = null!;
    
    public new Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return base.SaveChangesAsync(token);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OutboxMessageEntityConfiguration());
        modelBuilder.ApplyConfiguration(new MouseEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CatEntityConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}