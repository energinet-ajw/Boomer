namespace PetStore.Infrastructure.EventDispatching.Integration;

public interface IIntegrationEventDispatcher
{
    Task DispatchAsync();
}