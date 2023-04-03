using MediatR;
using PetStore.Application.Base;

namespace PetStore.Infrastructure.Pipelines;

public class UnitOfWorkPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkPipeline(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken token)
    {
        await Console.Out.WriteLineAsync("Calling next on UnitOfWorkPipeline...");
        var result = await next().ConfigureAwait(false);
        await _unitOfWork.CommitAsync(token).ConfigureAwait(false);
        await Console.Out.WriteLineAsync("Committed!");
        return result;
    }
}