using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetStore.Application.Cat;
using PetStore.Application.Cat.Queries;
using PetStore.Application.Mouse;
using PetStore.Application.Mouse.Queries;

namespace PetStore.Api.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CatsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public CatsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Get cats.
    /// </summary>
    [HttpGet]
    public async Task<IList<CatDto>> GetCats(CancellationToken token)
    {
        return await _mediator.Send(new GetCatsQuery(), token);
    }
}