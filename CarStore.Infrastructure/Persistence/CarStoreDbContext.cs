using CarStore.Infrastructure.Persistence.Car;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Infrastructure.Persistence;

public class CarStoreDbContext : DbContext, ICarStoreDbContext
{
    public CarStoreDbContext(DbContextOptions<CarStoreDbContext> options)
        : base(options)
    {
        // TODO - Removed when using real database
        Database.EnsureCreated();
    }

    // Added to support Moq in tests
    public CarStoreDbContext()
    {
    }

    public DbSet<Application.Cars.Car> Cars { get; private set; } = null!;
    
    public new Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return base.SaveChangesAsync(token);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CatEntityConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}