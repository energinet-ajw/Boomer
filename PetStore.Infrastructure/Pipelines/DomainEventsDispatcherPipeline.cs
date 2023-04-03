using MediatR;
using PetStore.Infrastructure.EventDispatching;

namespace PetStore.Infrastructure.Pipelines;

public class DomainEventsDispatcherPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public DomainEventsDispatcherPipeline(IDomainEventDispatcher domainEventDispatcher)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken token)
    {
        await Console.Out.WriteLineAsync("Calling next on DomainEventsDispatcher...").ConfigureAwait(false);
        var result = await next().ConfigureAwait(false);
        await _domainEventDispatcher.DispatchAsync(token).ConfigureAwait(false);
        await Console.Out.WriteLineAsync("Dispatched domain events!");
        return result;
    }
}