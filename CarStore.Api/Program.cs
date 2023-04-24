using CarStore.Infrastructure.Persistence;
using Wolverine;
using ISystemClock = Microsoft.Extensions.Internal.ISystemClock;

namespace CarStore.APi;

// ReSharper disable once ClassNeverInstantiated.Global
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<ISystemClock>(new Microsoft.Extensions.Internal.SystemClock());
        
        builder.Host.UseWolverine();
        
        builder.Services.AddTransient<CarRepository>();
        
        var app = builder.Build();
        
        app.MapPost("/cars", (CreateCar body, IMessageBus bus) => bus.InvokeAsync(body));
        
        
        // Adds middleware for redirecting HTTP requests to HTTPS  
        app.UseHttpsRedirection();
        //app.UseAuthentication();
        //app.UseAuthorization();
        await app.RunAsync();
    }
}