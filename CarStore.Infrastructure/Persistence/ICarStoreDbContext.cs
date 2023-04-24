namespace CarStore.Infrastructure.Persistence;

public interface ICarStoreDbContext
{
    Task<int> SaveChangesAsync(CancellationToken token = default);
}