﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Realtea.App.HttpContextWrapper;
using Realtea.App.Requests.Auth;
using Realtea.App.Responses.Auth;
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

        [HttpPost]
        [Route("generatetoken")]
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