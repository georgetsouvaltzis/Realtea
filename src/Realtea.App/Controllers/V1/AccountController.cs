using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Realtea.App.HttpContextWrapper;
using Realtea.Core.Commands.Account;

namespace Realtea.App.Controllers.V1
{
    [Authorize]
    public class AccountController : V1ControllerBase
    {
        public AccountController(IMediator mediator, IHttpContextAccessorWrapper httpContextAccessorWrapper) : base(mediator, httpContextAccessorWrapper)
        {
        }

        // TODO: probably will require another service to upgrade account.
        [HttpPatch]
        [Route("upgrade")]
        public async Task<ActionResult> UpgradeAccount()
        {
            //await _userService.UpgradeAccountAsync(User.FindFirstValue("sub"));

            return NoContent();
        }

        // TODO: ability to upgrade account details.
        [HttpPatch]
        public async Task<ActionResult> Edit([FromBody] EditAccountCommand command)
        {
            // Need to change Request/Response model.
            command.UserId = CurrentUserId;

            await Mediator.Send(command);

            return NoContent();
        }

        // TODO: User should be able to delete their account.
        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            await Mediator.Send(new DeleteAccountCommand { UserId = CurrentUserId });
            return NoContent();
        }
    }
}

