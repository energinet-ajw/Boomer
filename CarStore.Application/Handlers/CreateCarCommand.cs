using CarStore.Application.Base2;

namespace CarStore.Application.Handlers;

public class CreateCarCommand : ICommand<Guid>
{
    public CreateCarCommand(string name)
    {
        Name = name;
    }
    
    public string Name { get; }
}