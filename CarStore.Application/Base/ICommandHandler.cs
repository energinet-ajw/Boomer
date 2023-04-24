namespace CarStore.Application.Base2;

public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
{
}