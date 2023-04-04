using Microsoft.EntityFrameworkCore;
using PetStore.Domain.Base;
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
        var mouse = await _databaseContext.Mice.SingleOrDefaultAsync(x => x.Id == id, token).ConfigureAwait(false);
        if (mouse == null) throw new KeyNotFoundException(id.ToString());
        return mouse;
    }
    
    public async Task<IList<Domain.MouseAggregate.Mouse>> GetAllAsync(CancellationToken token)
    {
        return await _databaseContext.Mice.ToListAsync(token).ConfigureAwait(false);
    }

    public async Task<IList<Domain.MouseAggregate.Mouse>> GetBySpecificationAsync(Specification<Domain.MouseAggregate.Mouse> specification, CancellationToken token)
    {
        return await _databaseContext.Mice
            .Where(specification.ToExpression())
            .ToListAsync(token)
            .ConfigureAwait(false);
    }
}