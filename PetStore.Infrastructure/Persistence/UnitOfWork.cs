using PetStore.Application.Base;

namespace PetStore.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDatabaseContext _databaseContext;

    public UnitOfWork(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task CommitAsync(CancellationToken token = default)
    {
        await _databaseContext.SaveChangesAsync(token).ConfigureAwait(false);
    }
}