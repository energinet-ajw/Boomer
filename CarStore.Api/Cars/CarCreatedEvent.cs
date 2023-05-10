using CarStore.Application.Cars;

namespace CarStore.APi.Cars;

public class CarCreatedEvent
{
    public CarDto CarDto { get; set; } 

    public CarCreatedEvent(Car car)
    {
        CarDto = new CarDto
        {
            Id = car.Id,
            Name = car.Name
        };
    }
}