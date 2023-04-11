using MediatR;
using MediatR.Pipeline;
using PetStore.Application.Validators.Base;
using PetStore.Infrastructure.ExceptionHandling;
using PetStore.Infrastructure.Pipelines;

namespace PetStore.Api.Extensions;

public static class MediatorExtensions
{
    public static void AddMediator(this IServiceCollection serviceCollection)
    {
        // MediatR is a low-ambition library trying to solve a simple problem — decoupling the in-process sending of messages from handling messages.
        // By Jimmi Bogard, https://github.com/jbogard/MediatR
        serviceCollection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Root).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(PetStore.Application.Root).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(PetStore.Domain.Root).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(PetStore.Infrastructure.Root).Assembly);
        });
        
        // Pipeline behaviors
        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipeline<,>));
        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(DomainEventsDispatcherPipeline<,>));
        serviceCollection.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));
        
        // Request handlers 
        serviceCollection.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionLoggingHandler<,,>));
    }
}