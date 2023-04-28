using CarStore.Application.Base;

namespace CarStore.Application.Cars;

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<Guid> HandleAsync(TCommand createCarCommand);
}