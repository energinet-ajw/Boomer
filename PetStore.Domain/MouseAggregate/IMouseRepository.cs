namespace PetStore.Domain.MouseAggregate;

public interface IMouseRepository
{
    Task AddAsync(Mouse mouse);
    
    Task<Mouse?> GetAsync(Guid id, CancellationToken token);

    Task<IList<Domain.MouseAggregate.Mouse>> GetAllAsync(CancellationToken token);
}