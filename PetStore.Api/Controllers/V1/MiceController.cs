using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PetStore.Api.Options;
using PetStore.Application.Mouse;
using PetStore.Application.Mouse.Queries;

namespace PetStore.Api.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class MiceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOptions<MySecrets> _mySecrets;

    public MiceController(IMediator mediator, IOptions<MySecrets> mySecrets)
    {
        _mediator = mediator;
        _mySecrets = mySecrets;
    }

    /// <summary>
    ///     Get mice.
    /// </summary>
    [HttpGet]
    public async Task<IList<MouseDto>> GetMice(CancellationToken token)
    {
        Console.Out.WriteLine("AJW " + _mySecrets.Value.AspnetcoreEnvironment);
        return await _mediator.Send(new GetMiceQuery(), token);
    }

    /// <summary>
    ///     Create mouse.
    /// </summary>
    [HttpPost]
    public async Task<Guid> CreateMouse([FromBody] string name, CancellationToken token)
    {
        return await _mediator.Send(new CreateMouseCommand(name), token);
    }

    /// <summary>
    ///     Get Mouse.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<MouseDto> GetMouse(Guid id, CancellationToken token)
    {
        return await _mediator.Send(new GetMouseQuery(id), token);
    }
    
    /// <summary>
    ///     Search mice.
    /// </summary>
    [HttpGet("search")]
    public async Task<IList<MouseDto>> SearchMice([FromQuery] string name, CancellationToken token)
    {
        return await _mediator.Send(new SearchMiceQuery(name), token);
    }
}