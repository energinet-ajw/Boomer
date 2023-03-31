using PetStore.Application.Base;

namespace PetStore.Application.Mouse
{
    public class CreateMouseCommand : ICommand<Guid> {

        public CreateMouseCommand()
        {
        }
    }

    public class CreateMouseCommandHandler : ICommandHandler<CreateMouseCommand, Guid>
    {
        public async Task<Guid> Handle(CreateMouseCommand command, CancellationToken cancellationToken)
        {
            var mouse = new Boomer.Domain.Mouse();
            return await Task.FromResult(mouse.Id);
        }
    }
}
