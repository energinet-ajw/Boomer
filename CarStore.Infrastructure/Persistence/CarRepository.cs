using CarStore.Application.Handlers;

namespace CarStore.Infrastructure.Persistence;

public class CarRepository : ICarRepository
{
    private readonly CarStoreDbContext _context;
    
    public CarRepository(CarStoreDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> AddAsync(Application.Handlers.Car car)
    {
        await _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();
        return car.Id;
    }
}