using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetStore.Application.Mouse;

namespace Boomer.WebApi.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BoomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BoomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get hello v1.
        /// </summary>
        [HttpGet]
        public async Task<string> GetHello(CancellationToken token)
        {
            return await _mediator.Send(new GetHelloQuery(), token);
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
        ///     Get boomer. 
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBoomer(string id)
        {
            return await Task.FromResult(Ok("Okay, Boomer"));
        }
    }
}



