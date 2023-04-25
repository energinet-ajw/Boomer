using CarStore.Application.Base2;

namespace CarStore.Application.Base;

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
}