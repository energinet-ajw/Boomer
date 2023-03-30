using Boomer.Application.Base;
using Boomer.Application.Commands;
using MediatR;

namespace Boomer.Application.Mouse
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
            var mouse = new Domain.Mouse();
            return await Task.FromResult(mouse.Id);
        }
    }
}
