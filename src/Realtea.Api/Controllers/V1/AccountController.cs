using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realtea.Api;
using Realtea.Api.Attributes;
using Realtea.Api.Examples;
using Realtea.App.HttpContextWrapper;
using Realtea.App.Requests.Account;
using Realtea.Core.Commands.Account;
using Realtea.Core.Commands.Payment;
using Realtea.Infrastructure.Commands.User;
using Swashbuckle.AspNetCore.Filters;

namespace Realtea.App.Controllers.V1
{
    public class AccountController : V1ControllerBase
    {
        public AccountController(IMediator mediator, IHttpContextAccessorWrapper httpContextAccessorWrapper) : base(mediator, httpContextAccessorWrapper)
        {
        }

        [HttpPatch]
        [BearerAuthorize]
        [Route("upgrade")]
        public async Task<ActionResult> UpgradeAccount()
        {
            await Mediator.Send(new AddUserToBrokerRoleCommand { UserId = CurrentUserId });

            return NoContent();
        }

        [HttpPatch]
        [BearerAuthorize]
        [SwaggerRequestExample(typeof(EditAccountRequest), typeof(EditAccountRequestExample))]
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
        [BearerAuthorize]
        public async Task<ActionResult> Delete()
        {
            await Mediator.Send(new DeleteAccountCommand { UserId = CurrentUserId });
            return NoContent();
        }

        [HttpPost]
        [BearerAuthorize]
        [Route("add-balance")]
        public async Task<ActionResult> AddBalance()
        {
            await Mediator.Send(new AddBalanceCommand { UserId = CurrentUserId });

            return NoContent();
        }
    }
}

