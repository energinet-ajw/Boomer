using MediatR;

namespace Boomer.Application.Commands
{
    public class OneWayCommand : IRequest { }

    public class OneWayCommandHandler : AsyncRequestHandler<OneWayCommand>
    {
        private readonly IMediator _mediator;

        public OneWayCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        protected override async Task Handle(OneWayCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new MyEvent(), cancellationToken);
        }
    }
}
