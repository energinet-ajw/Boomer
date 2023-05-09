using CarStore.APi.Routes;
using CarStore.Application.Cars;
using CarStore.Infrastructure.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Wolverine;
using ISystemClock = Microsoft.Extensions.Internal.ISystemClock;

namespace CarStore.APi;

// ReSharper disable once ClassNeverInstantiated.Global
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        
        var inMemorySqlite = new SqliteConnection(builder.Configuration.GetConnectionString("CarStore"));
        inMemorySqlite.Open();
        builder.Services.AddDbContext<CarStoreDbContext>(options => {
            options.UseSqlite(inMemorySqlite);
        });

        
        builder.Services.AddSingleton<ISystemClock>(new Microsoft.Extensions.Internal.SystemClock());
        
        builder.Host.UseWolverine();
        
        builder.Services.AddTransient<ICarRepository, CarRepository>();
        
        var app = builder.Build();
        
        app.AddRoutes();
        
        
        // Adds middleware for redirecting HTTP requests to HTTPS  
        app.UseHttpsRedirection();
        //app.UseAuthentication();
        //app.UseAuthorization();
        await app.RunAsync();
    }
}