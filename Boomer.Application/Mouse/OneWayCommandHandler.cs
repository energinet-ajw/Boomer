using Boomer.Application.Base;
using MediatR;

namespace Boomer.Application.Mouse
{
    public class OneWayCommand : ICommand { }

    public class OneWayCommandHandler : ICommandHandler<OneWayCommand>
    {
        private readonly IMediator _mediator;

        public OneWayCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
 
        public async Task Handle(OneWayCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new MyEvent(), cancellationToken);
        }
    }
}
