namespace CarStore.Application.Cars;

public class CreateCarHandler : ICommandHandler<CreateCarCommand, Guid>
{
    private readonly ICarRepository _carRepository;

    public CreateCarHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }
    
    public async Task<Guid> HandleAsync(CreateCarCommand command)
    {
        var car = new Car(command.Name);
        var id = await _carRepository.AddAsync(car);
        return id;
    }
}