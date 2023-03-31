using MediatR;

namespace Boomer.Domain;

public interface IEvent : INotification
{
}

public interface IDomainEvent : IEvent
{
}