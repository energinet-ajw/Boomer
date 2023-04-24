using CarStore.Application.Base2;

namespace CarStore.Application.Handlers;

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<Guid> HandleAsync(TCommand createCarCommand);
}