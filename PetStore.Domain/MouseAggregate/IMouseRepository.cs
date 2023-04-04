using PetStore.Domain.Base;

namespace PetStore.Domain.MouseAggregate;

public interface IMouseRepository
{
    Task AddAsync(Mouse mouse);
    
    Task<Mouse> GetAsync(Guid id, CancellationToken token);

    Task<IList<Mouse>> GetAllAsync(CancellationToken token);
    
    Task<IList<Mouse>> GetBySpecificationAsync(Specification<Mouse> specification, CancellationToken token);
}