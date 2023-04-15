using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Realtea.Core.Entities;
using Realtea.Infrastructure.Commands.Authorization;

namespace Realtea.App.Controllers.V1
{
    public class AuthController : V1ControllerBase
    {
        private IMediator _mediator;
       
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserCommand command)
        {
            await _mediator.Send(command);
           
            // TODO: Try to send email?


            return Ok();
        }

        [HttpPost]
        [Route("generatetoken")]
        public async Task<ActionResult> GenerateToken([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
