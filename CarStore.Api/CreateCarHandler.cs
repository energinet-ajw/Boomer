using CarStore.Application.Cars;
using CarStore.Infrastructure.Persistence;

namespace CarStore.APi;

public record CreateCarCommand(string Model);

public class CreateCarHandler
{
    private readonly CarRepository _carRepository;

    public CreateCarHandler(CarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public CarCreatedEvent Consume(CreateCarCommand command)
    {
        var car = new Car("Ford");
       return new CarCreatedEvent(car.Id);
    }
}

public class CarCreatedEvent
{
    public readonly Guid Id;

    public CarCreatedEvent(Guid id)
    {
        Id = id;
    }
}

public class CarCreatedEventHandler
{
    public Guid Handle(CarCreatedEvent carCreatedEvent, ICarRepository carRepository)
    {
        Console.WriteLine("HERE " + carCreatedEvent.Id);
        return carCreatedEvent.Id;
    }
}