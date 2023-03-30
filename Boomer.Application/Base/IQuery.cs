using MediatR;

namespace Boomer.Application.Base
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
