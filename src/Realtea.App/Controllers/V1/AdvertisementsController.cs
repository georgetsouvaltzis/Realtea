using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realtea.App.Models;
using Realtea.Core.Interfaces;
using Realtea.Core.Models;
using Realtea.Core.Enums;
using Realtea.App.Enums;
using MediatR;
using Realtea.Core.Queries;
using Realtea.App.HttpContextWrapper;
using Realtea.Core.Commands.Advertisement;
using Realtea.Core.Requests;
using Realtea.App.Identity.Authorization.Requirements.Advertisement;

namespace Realtea.App.Controllers.V1
{
    public class AdvertisementsController : V1ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessorWrapper _httpContextAccessorWrapper;

        public AdvertisementsController(
            IAuthorizationService authorizationService,
            IMediator mediator,
            IHttpContextAccessorWrapper httpContextAccessorWrapper)
        {
            _mediator = mediator;
            _authorizationService = authorizationService;
            _httpContextAccessorWrapper = httpContextAccessorWrapper;
        }

        // should update Based on IsActive, Desc/asc, etc.
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] ReadFilteredAdvertisementsQuery request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] ReadAdvertisementQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        // currently it uses no authorized value of ID. Need to refactor it.
        // Probably add event that in case user adds Paid ad,
        // Make it appear on top?
        public async Task<ActionResult> Add([FromBody] CreateAdvertisementRequest request)
        {
            var userId = _httpContextAccessorWrapper.GetUserId();

            var command = new CreateAdvertisementCommand
            {
                Name = request.Name,
                AdvertisementType = request.AdvertisementType,
                Description = request.Description,
                IsActive = request.IsActive,
                UserId = userId,
                // TODO: Need to change this.S
                CreateAdvertisementDetails = request.UpdateAdvertisementDetails,
            };

            var resultt = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = resultt.Id }, new ReadAdvertisementQuery { Id = resultt.Id });
        }

        [HttpDelete]
        [Authorize]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] DeleteAdvertisementCommand request)
        {
            var existingAd = await _mediator.Send(new ReadAdvertisementQuery { Id = request.Id });

            var result = await _authorizationService.AuthorizeAsync(User, existingAd, new IsEligibleForAdvertisementDeleteRequirement());

            if (!result.Succeeded)
            {
                return Forbid();
            }

            await _mediator.Send(request);

            return NoContent();
        }

        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateAdvertisementCommand request)
        {
            var existingAd = await _mediator.Send(new ReadAdvertisementQuery { Id = id });

            var result = await _authorizationService.AuthorizeAsync(User, existingAd, new IsEligibleForAdvertisementUpdateRequirement());

            if (!result.Succeeded)
            {
                return Forbid();
            }

            var userId = _httpContextAccessorWrapper.GetUserId();

            // Need To change to request/response model + Command model

            request.Id = id;

            var updatedAd = await _mediator.Send(request);

            return Ok(updatedAd);
        }
    }
}
