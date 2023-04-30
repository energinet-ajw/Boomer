namespace CarStore.Application.Cars;

public interface IBatchRepository
{
    Task<Guid> AddAsync(Batch batch);
}