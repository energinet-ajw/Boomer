using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetStore.Application.Mouse;

namespace PetStore.Api.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class MouseController : ControllerBase
{
    private readonly IMediator _mediator;

    public MouseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Get mice.
    /// </summary>
    [HttpGet]
    public async Task<IList<MouseDto>> GetMice(CancellationToken token)
    {
        return await _mediator.Send(new GetMiceQuery(), token);
    }

    /// <summary>
    ///     Create mouse.
    /// </summary>
    [HttpPost]
    public async Task<Guid> CreateMouse(CancellationToken token)
    {
        return await _mediator.Send(new CreateMouseCommand(), token);
    }

    /// <summary>
    ///     Get Mouse.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<MouseDto> GetMouse(Guid id, CancellationToken token)
    {
        return await _mediator.Send(new GetMouseQuery(id), token);
    }
}