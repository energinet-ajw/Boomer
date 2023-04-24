using System;
using System.Threading.Tasks;
using CarStore.Application.Handlers;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace CarStore.Functions.Functions;

public class AddCarTrigger
{
    private readonly ICommandHandler<CreateCarCommand, Guid> _handler;

    public AddCarTrigger(ICommandHandler<CreateCarCommand, Guid> handler)
    {
        _handler = handler;
    }
    
    [FunctionName("AddCarTrigger")]
    public async Task RunAsync([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
    {
        log.LogInformation($"C# Timer trigger function executed at: {DateTime.UtcNow}");
        await _handler.HandleAsync(new CreateCarCommand());
    }
}