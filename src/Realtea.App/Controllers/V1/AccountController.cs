using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Realtea.Core.Services;
using Realtea.Domain.Entities;

namespace Realtea.App.Controllers.V1
{
    [Authorize]
    public class AccountController : V1ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        // TODO: probably will require another service to upgrade account.
        [HttpPatch]
        [Route("upgrade")]
        public async Task<ActionResult> UpgradeAccount()
        {
            await _userService.UpdateAccountAsync(User.FindFirstValue("sub"));

            return NoContent();
        }

        // TODO: ability to upgrade account details.
        [HttpPatch]
        public async Task<ActionResult> Edit()
        {
            return Ok();
        }

        // TODO: User should be able to delete their account.
        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            return Ok();
        }
    }
}

