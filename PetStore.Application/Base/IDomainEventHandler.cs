using MediatR;
using PetStore.Domain.Base;

namespace PetStore.Application.Base;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}