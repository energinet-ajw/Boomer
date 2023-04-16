using PetStore.Application.Base;
using PetStore.Domain.MouseAggregate;

namespace PetStore.Application.Mouse;

public class CreateMouseCommand : ICommand<Guid>
{
    public CreateMouseCommand(string name)
    {
        Name = name;
    }
    public string Name { get; }
}

public class CreateMouseCommandHandler : ICommandHandler<CreateMouseCommand, Guid>
{
    private readonly IMouseRepository _mouseRepository;

    public CreateMouseCommandHandler(IMouseRepository mouseRepository)
    {
        _mouseRepository = mouseRepository;
    }

    public async Task<Guid> Handle(CreateMouseCommand command, CancellationToken cancellationToken)
    {
        await Console.Out.WriteLineAsync($"Handling {command.GetType().Name}...").ConfigureAwait(false);
        var mouse = new Domain.MouseAggregate.Mouse("Jerry");
        await _mouseRepository.AddAsync(mouse).ConfigureAwait(false);
        return mouse.Id;
    }
}