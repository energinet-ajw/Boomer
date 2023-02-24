using MediatR;

namespace Boomer.Application.Commands.Base
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
