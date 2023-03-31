namespace PetStore.Domain.MouseAggregate;

public interface IMouseRepository
{
    Task AddAsync(Mouse mouse);
}