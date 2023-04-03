using MediatR;
using PetStore.Application.Validators.Base;
using PetStore.Infrastructure.Pipelines;

namespace PetStore.Api.Extensions;

public static class PipelineBehaviours
{
    public static void AddPipelineBehaviours(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipeline<,>));
        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(DomainEventsDispatcherPipeline<,>));
        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
    }
}