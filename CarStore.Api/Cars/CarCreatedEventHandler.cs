using CarStore.Application.Cars;

namespace CarStore.APi.Cars;

public class CarCreatedEventHandler
{
    public void Handle(CarCreatedEvent carCreatedEvent, ICarRepository carRepository)
    {
        Console.WriteLine("HERE " + carCreatedEvent.CarDto.Id);
    }
}