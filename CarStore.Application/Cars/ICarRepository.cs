namespace CarStore.Application.Cars;

public interface ICarRepository
{
    Task<Guid>AddAsync(Car car);
}