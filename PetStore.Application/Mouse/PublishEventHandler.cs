using MediatR;
using PetStore.Application.Base;

namespace PetStore.Application.Mouse;

public class PublishEventCommand : ICommand
{
}

public class PublishEventHandler : ICommandHandler<PublishEventCommand>
{
    private readonly IMediator _mediator;

    public PublishEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(PublishEventCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new MyEvent(), cancellationToken).ConfigureAwait(false);
    }
}