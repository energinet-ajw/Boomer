using MediatR;

namespace Boomer.Application.Queries.Base
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
