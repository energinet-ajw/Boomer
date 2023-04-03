using Microsoft.EntityFrameworkCore;
using PetStore.Domain.MouseAggregate;

namespace PetStore.Infrastructure.Persistence.Mouse;

public class MouseRepository : IMouseRepository
{
    private readonly IDatabaseContext _databaseContext;

    public MouseRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task AddAsync(Domain.MouseAggregate.Mouse mouse)
    {
        await _databaseContext.Mice.AddAsync(mouse);
    }

    public async Task<Domain.MouseAggregate.Mouse> GetAsync(Guid id, CancellationToken token)
    {
        return await _databaseContext.Mice.SingleOrDefaultAsync(x => x.Id == id, token).ConfigureAwait(false);
    }
    
    public async Task<IList<Domain.MouseAggregate.Mouse>> GetAllAsync(CancellationToken token)
    {
        return await _databaseContext.Mice.ToListAsync(token).ConfigureAwait(false);
    }
}