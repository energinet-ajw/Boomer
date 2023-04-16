using Microsoft.EntityFrameworkCore;
using PetStore.Domain.CatAggregate;

namespace PetStore.Infrastructure.Persistence.Cat;

public class CatRepository : ICatRepository
{
    private readonly IDatabaseContext _databaseContext;

    public CatRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task AddAsync(Domain.CatAggregate.Cat cat)
    {
        await _databaseContext.Cats.AddAsync(cat);
    }
    
    public async Task<Domain.CatAggregate.Cat> GetAsync(Guid id, CancellationToken token)
    {
        var cat = await _databaseContext.Cats.SingleOrDefaultAsync(x => x.Id == id, token).ConfigureAwait(false);
        if (cat == null) throw new KeyNotFoundException(id.ToString());
        return cat;
    }

    public async Task<IList<Domain.CatAggregate.Cat>> GetAllAsync(CancellationToken token)
    {
        return await _databaseContext.Cats.ToListAsync(token).ConfigureAwait(false);
    }
}