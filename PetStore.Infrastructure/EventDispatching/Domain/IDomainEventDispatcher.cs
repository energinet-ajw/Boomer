namespace PetStore.Infrastructure.EventDispatching.Domain;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(CancellationToken token);
}