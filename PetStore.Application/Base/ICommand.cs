using MediatR;

namespace PetStore.Application.Base;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}

public interface ICommand : IRequest
{
}