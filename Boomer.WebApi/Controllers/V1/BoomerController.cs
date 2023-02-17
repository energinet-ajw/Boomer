using Boomer.Application.Commands;
using Boomer.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<string> GetHello()
        {
            return await _mediator.Send(new GetHelloQuery());
        }
        
        /// <summary>
        ///     Sends a OneWay command.
        /// </summary>
        [HttpPost("functions/send")]
        public async Task SendOneWay()
        {
            await _mediator.Send(new OneWayCommand());
        }
        
        /// <summary>
        ///     Sends a OneWay command from parameter.
        /// </summary>
        [HttpPost("functions/sendOneWay")]
        public async Task SendOneWay(OneWayCommand command)
        {
            await _mediator.Send(command);
        }
        
        /// <summary>
        /// Returns boomer. 
        /// </summary>
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult> GetBoomer(string id)
        {
            //return Forbid();
            return await Task.FromResult(Ok("Okay, Boomer"));
        }
    }
}



