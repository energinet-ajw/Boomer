using MediatR;

namespace PetStore.Infrastructure.EventDispatching;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;
    private readonly IDomainEventContainer _domainEventContainer;

    public DomainEventDispatcher(IMediator mediator, IDomainEventContainer domainEventContainer)
    {
        _mediator = mediator;
        _domainEventContainer = domainEventContainer;
    }
    public async Task DispatchAsync(CancellationToken token)
    {
        var domainEvents = _domainEventContainer.GetAllDomainEvents();
        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent, token).ConfigureAwait(false);
        }

        _domainEventContainer.ClearAllDomainEvents();
    }
}