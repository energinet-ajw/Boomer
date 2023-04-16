using MediatR;

namespace PetStore.Infrastructure.EventDispatching.Domain;

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
        var domainEvents = await _domainEventContainer.GetAllDomainEventsAsync();
        
        var tasks = new List<Task>();
        while (domainEvents.Any())
        {
            await _domainEventContainer.ClearAllDomainEvents();

            tasks.AddRange(domainEvents
                .Select(async (domainEvent) => {
                    await Console.Out.WriteLineAsync($"Dispatching domain event {domainEvent.GetType()}");
                    await _mediator.Publish(domainEvent, token).ConfigureAwait(false);
                }));

            domainEvents = await _domainEventContainer.GetAllDomainEventsAsync();
        }
        await Task.WhenAll(tasks);
    }
}