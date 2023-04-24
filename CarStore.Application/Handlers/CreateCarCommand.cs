using CarStore.Application.Base2;

namespace CarStore.Application.Handlers;

public class CreateCarCommand : ICommand<Guid>
{
    public string Name { get; set; }
}