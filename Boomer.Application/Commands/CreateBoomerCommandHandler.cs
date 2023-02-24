using Boomer.Application.Commands.Base;
using MediatR;

namespace Boomer.Application.Commands
{
    public class CreateBoomerCommand : ICommand {

        public CreateBoomerCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class CreateBoomerCommandHandler : ICommandHandler<CreateBoomerCommand>
    {
        public async Task<Unit> Handle(CreateBoomerCommand command, CancellationToken cancellationToken)
        {
            var boomer = new Domain.Boomer(command.Id);
            await Task.FromResult("");
            return Unit.Value;
        }
    }
}
