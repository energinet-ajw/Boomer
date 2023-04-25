using System;
using System.Threading.Tasks;
using CarStore.Application.Handlers;
using CarStore.Infrastructure.Persistence;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarStore.Functions;

public static class Program
{
    public static async Task Main()
    {
       
        
        var host = new HostBuilder()
            // Used to add settings required for the function app to run in an isolated worker process
            .ConfigureFunctionsWorkerDefaults(builder =>
            {
                builder
                    .AddApplicationInsights();
                //.AddApplicationInsightsLogger();
            })
            .ConfigureServices(s =>
            {
                s.AddTransient<ICommandHandler<CreateCarCommand, Guid>, CreateCarHandler>();
                s.AddTransient<ICarRepository, CarRepository>();

                s.AddDbContext<CarStoreDbContext>(options =>
                {
                    // Get IConfiguration
                    var configuration = s.BuildServiceProvider().GetService<IConfiguration>();
                    var inMemorySqlite = new SqliteConnection(configuration.GetConnectionString("CarStore"));
                    inMemorySqlite.Open();
                    options.UseSqlite(inMemorySqlite);
                });
            })
            .Build();
        
        await host.RunAsync().ConfigureAwait(false);
    }
}





  


