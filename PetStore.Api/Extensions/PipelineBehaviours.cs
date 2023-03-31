using Boomer.Application.Validators.Base;
using MediatR;

namespace PetStore.Api.Extensions;

public static class PipelineBehaviours
{
    public static void AddMediatorPipelineBehaviours(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    }
}