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
}