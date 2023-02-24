using MediatR;

namespace Boomer.Application.Commands.Base
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }

    public interface ICommand : IRequest
    {
    }
}
