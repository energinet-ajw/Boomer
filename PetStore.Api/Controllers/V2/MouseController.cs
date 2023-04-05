using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetStore.Application.Mouse;
using PetStore.Application.Mouse.Queries;

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
    ///     Get Mouse v2.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<MouseDto> GetMouse(Guid id, CancellationToken token)
    {
        return await _mediator.Send(new GetMouseQuery(id), token);
    }
}