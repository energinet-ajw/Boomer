using MediatR;

namespace PetStore.Application.Base
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
