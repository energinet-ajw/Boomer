using Boomer.Application.Base;
using MediatR;

namespace Boomer.Application.Commands.Base;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IEvent
{
}