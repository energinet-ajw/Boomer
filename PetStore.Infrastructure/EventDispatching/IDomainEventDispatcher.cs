using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace PetStore.Infrastructure.EventDispatching;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(CancellationToken token);
}