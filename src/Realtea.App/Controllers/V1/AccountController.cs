using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realtea.App.HttpContextWrapper;
using Realtea.App.Requests.Account;
using Realtea.Core.Commands.Account;
using Realtea.Infrastructure.Commands.User;

namespace Realtea.App.Controllers.V1
{
    [Authorize]
    public class AccountController : V1ControllerBase
    {
        public AccountController(IMediator mediator, IHttpContextAccessorWrapper httpContextAccessorWrapper) : base(mediator, httpContextAccessorWrapper)
        {
        }

        [HttpPatch]
        [Route("upgrade")]
        public async Task<ActionResult> UpgradeAccount()
        {
            await Mediator.Send(new AddUserToBrokerRoleCommand { UserId = CurrentUserId });

            return NoContent();
        }

        [HttpPatch]
        public async Task<ActionResult> Edit([FromBody] EditAccountRequest request)
        {
            var command = new EditAccountCommand
            {
                UserId = CurrentUserId,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            await Mediator.Send(new DeleteAccountCommand { UserId = CurrentUserId });
            return NoContent();
        }
    }
}

