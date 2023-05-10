using CarStore.Application.Cars;
using CarStore.Infrastructure.Persistence;
using Wolverine;

namespace CarStore.APi.Cars;

public class CreateCarHandler
{
    private readonly CarRepository _carRepository;

    public CreateCarHandler(CarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public OutgoingMessages Handle(CreateCarCommand command)
    {
        var car = new Car(command.Name);
        
        var messages = new OutgoingMessages
        {
            new CarCreatedEvent(car),
            car
        };
        
        messages.RespondToSender(car);
        
        return messages;
    }
}