using CarStore.Application.Base;

namespace CarStore.Application.Cars;

public class CreateCarCommand : ICommand<Guid>
{
    public CreateCarCommand(string name)
    {
        Name = name;
    }
    
    public string Name { get; }
}