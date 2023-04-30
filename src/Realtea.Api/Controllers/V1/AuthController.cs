using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Realtea.Api.Examples;
using Realtea.App.HttpContextWrapper;
using Realtea.App.Requests.Auth;
using Realtea.App.Responses.Auth;
using Realtea.Infrastructure.Commands.Authorization;
using Swashbuckle.AspNetCore.Filters;

namespace Realtea.App.Controllers.V1
{
    /// <summary>
    /// Responsible for User Registration/Token generation.
    /// </summary>
    public class AuthController : V1ControllerBase
    {
        public AuthController(IMediator mediator, IHttpContextAccessorWrapper wrapper) : base(mediator, wrapper)
        {
        }

        /// <summary>
        /// Registers new user.
        /// </summary>
        /// <param name="request">Neccessary data for user registration.</param>
        /// <returns>Sucessful response if User is registered.</returns>
        [HttpPost]
        [Route("register")]
        [SwaggerRequestExample(typeof(RegisterUserRequest), typeof(RegisterUserRequestExample))]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(void))]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var command = new RegisterUserCommand
            {
                UserName = request.UserName,
                Password = request.Password,
                ConfirmedPassword = request.ConfirmedPassword,
            };

            await Mediator.Send(command);
            // TODO: Try to send email?
            return Ok();
        }

        /// <summary>
        /// Generates JWT token for registered user.
        /// </summary>
        /// <param name="request">Contains user credentials to log in and generate JWT token For Authorized actions.</param>
        /// <returns>Successful response with generated token.</returns>
        [HttpPost]
        [Route("generatetoken")]
        [SwaggerRequestExample(typeof(LoginUserRequest), typeof(LoginUserRequestExample))]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(LoginUserResponse))]
        [ProducesResponseType((int) HttpStatusCode.BadRequest, Type = typeof(void))]
        [ProducesResponseType((int) HttpStatusCode.NotFound, Type = typeof(void))]
        public async Task<ActionResult> GenerateToken([FromBody] LoginUserRequest request)
        {
            var command = new LoginUserCommand
            {
                UserName = request.UserName,
                Password = request.Password,
            };

            var result = await Mediator.Send(command);

            return Ok(new LoginUserResponse
            {
                Token = result.Token
            });
        }
    }
}
