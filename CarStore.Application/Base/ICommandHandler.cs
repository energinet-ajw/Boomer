namespace CarStore.Application.Base;

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
    Task<Guid> HandleAsync(TCommand createCarCommand);
}