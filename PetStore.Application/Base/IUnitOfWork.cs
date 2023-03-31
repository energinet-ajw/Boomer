namespace PetStore.Application.Base;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken token = default);
}