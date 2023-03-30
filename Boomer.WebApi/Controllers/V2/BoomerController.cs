using Boomer.Application.Boomer;
using Boomer.Application.Mouse;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boomer.WebApi.Controllers.V2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    public class BoomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BoomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get hello v2.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetHello()
        {
            return await _mediator.Send(new GetHelloQuery());
        }
        
        /// <summary>
        ///     Sends a OneWay command v2.
        /// </summary>
        /// <returns>OK</returns>
        [HttpPost("functions/send")]
        public async Task SendOneWay()
        {
            await _mediator.Send(new OneWayCommand());
        }
        
        /// <summary>
        ///     Sends a OneWay command from parameter v2.
        /// </summary>
        /// <param name="command">The command.</param>
        [HttpPost("functions/sendOneWay")]
        public async Task SendOneWay(OneWayCommand command)
        {
            await _mediator.Send(command);
        }
    }
}



