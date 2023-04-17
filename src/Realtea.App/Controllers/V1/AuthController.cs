using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Realtea.App.HttpContextWrapper;
using Realtea.Core.Entities;
using Realtea.Infrastructure.Commands.Authorization;

namespace Realtea.App.Controllers.V1
{
    public class AuthController : V1ControllerBase
    {  
        public AuthController(IMediator mediator, IHttpContextAccessorWrapper wrapper) : base(mediator, wrapper)
        {
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserCommand command)
        {
            await Mediator.Send(command);
            // TODO: Try to send email?
            return Ok();
        }

        [HttpPost]
        [Route("generatetoken")]
        public async Task<ActionResult> GenerateToken([FromBody] LoginUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
