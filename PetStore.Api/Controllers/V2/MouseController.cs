using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetStore.Application.Mouse;

namespace PetStore.Api.Controllers.V2;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
public class MouseController : ControllerBase
{
    private readonly IMediator _mediator;

    public MouseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Sends a OneWay command v2.
    /// </summary>
    /// <returns>OK</returns>
    [HttpPost("functions/send")]
    public async Task SendOneWay()
    {
        await _mediator.Send(new PublishEventCommand());
    }

    /// <summary>
    ///     Sends a OneWay command from parameter v2.
    /// </summary>
    /// <param name="command">The command.</param>
    [HttpPost("functions/sendOneWay")]
    public async Task SendOneWay(PublishEventCommand command)
    {
        await _mediator.Send(command);
    }
}