namespace CarStore.Application.Handlers;

public interface ICarRepository
{
    Task<Guid>AddAsync(Car car);
}