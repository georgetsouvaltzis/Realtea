using System.Net;
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
    /// <summary>
    /// Account-related actions.
    /// </summary>
    public class AccountController : V1ControllerBase
    {
        public AccountController(IMediator mediator, IHttpContextAccessorWrapper httpContextAccessorWrapper) : base(mediator, httpContextAccessorWrapper)
        {
        }

        /// <summary>
        /// Upgrade user's account from Normal to Broker
        /// </summary>
        /// <returns>Successful response</returns>
        [HttpPatch]
        [BearerAuthorize]
        [ProducesResponseType((int) HttpStatusCode.NoContent, Type = typeof(void))]
        [ProducesResponseType((int) HttpStatusCode.Conflict)]
        [Route("upgrade")]
        public async Task<ActionResult> UpgradeAccount()
        {
            await Mediator.Send(new AddUserToBrokerRoleCommand { UserId = CurrentUserId });

            return NoContent();
        }

        /// <summary>
        /// Updates account details.
        /// </summary>
        /// <param name="request">Data to update.</param>
        /// <returns>Successful response,</returns>
        [HttpPatch]
        [BearerAuthorize]
        [SwaggerRequestExample(typeof(EditAccountRequest), typeof(EditAccountRequestExample))]
        [ProducesResponseType((int) HttpStatusCode.NoContent, Type =typeof(void))]
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

        /// <summary>
        /// Deactivates user account.
        /// </summary>
        /// <returns>Sucessful response.</returns>
        [HttpDelete]
        [BearerAuthorize]
        [ProducesResponseType((int) HttpStatusCode.NoContent, Type = typeof(void))]
        public async Task<ActionResult> Delete()
        {
            await Mediator.Send(new DeleteAccountCommand { UserId = CurrentUserId });
            return NoContent();
        }

        /// <summary>
        /// Adds balance to user account.
        /// </summary>
        /// <returns>Successful response.</returns>
        [HttpPost]
        [BearerAuthorize]
        [Route("add-balance")]
        [ProducesResponseType((int) HttpStatusCode.NoContent, Type = typeof(void))]
        public async Task<ActionResult> AddBalance()
        {
            await Mediator.Send(new AddBalanceCommand { UserId = CurrentUserId });

            return NoContent();
        }
    }
}

