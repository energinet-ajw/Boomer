using System;
using System.Threading.Tasks;
using CarStore.Application.Cars;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CarStore.Functions.Functions;

public class AddCarTrigger
{
    private readonly ICommandHandler<CreateCarCommand, Guid> _handler;

    public AddCarTrigger(ICommandHandler<CreateCarCommand, Guid> handler)
    {
        _handler = handler;
    }
    
    [Function("AddCarTrigger")]
    public async Task RunAsync([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
    {
        //log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");
        var id = await _handler.HandleAsync(new CreateCarCommand("Ford"));
    }
}