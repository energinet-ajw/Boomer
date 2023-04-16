namespace PetStore.Domain.CatAggregate;

public interface ICatRepository
{
    Task AddAsync(Cat cat);
    
    Task<Cat> GetAsync(Guid id, CancellationToken token);
    
    Task<IList<Cat>> GetAllAsync(CancellationToken token);
}