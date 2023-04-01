using MediatR;
using PetStore.Domain.Base;

namespace PetStore.Application.Base;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IEvent
{
}