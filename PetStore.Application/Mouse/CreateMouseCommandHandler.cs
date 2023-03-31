using PetStore.Application.Base;
using PetStore.Domain.MouseAggregate;

namespace PetStore.Application.Mouse;

public class CreateMouseCommand : ICommand<Guid>
{
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
        var mouse = new Domain.MouseAggregate.Mouse();
        await _mouseRepository.AddAsync(mouse).ConfigureAwait(false);
        return mouse.Id;
    }
}