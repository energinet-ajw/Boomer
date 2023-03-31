using Boomer.Domain;
using MediatR;

namespace PetStore.Application.Base;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IEvent
{
}