using Wolverine;

namespace CarStore.APi.Routes;

public static class Routes
{
    public static void AddRoutes(this WebApplication app)
    {
        app.MapPost("/cars", (CreateCarCommand body, IMessageBus bus) => bus.InvokeAsync<Guid>(body));

    }
}