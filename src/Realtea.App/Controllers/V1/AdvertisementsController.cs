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
        private readonly IAuthorizationService _authorizationService;

        public AdvertisementsController(IMediator mediator,
            IAuthorizationService authorizationService,
            IHttpContextAccessorWrapper wrapper) : base(mediator, wrapper)
        {
            _authorizationService = authorizationService; 
        }

        // should update Based on IsActive, Desc/asc, etc.
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] ReadFilteredAdvertisementsQuery request)
        {
            var result = await Mediator.Send(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] ReadAdvertisementQuery request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Add([FromBody] CreateAdvertisementRequest request)
        {
            var command = new CreateAdvertisementCommand
            {
                Name = request.Name,
                AdvertisementType = request.AdvertisementType,
                Description = request.Description,
                IsActive = request.IsActive,
                UserId = CurrentUserId,
                CreateAdvertisementDetails = request.UpdateAdvertisementDetails,
            };

            var resultt = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = resultt.Id }, new ReadAdvertisementQuery { Id = resultt.Id });
        }

        [HttpDelete]
        [Authorize]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] DeleteAdvertisementCommand request)
        {
            var existingAd = await Mediator.Send(new ReadAdvertisementQuery { Id = request.Id });

            var result = await _authorizationService.AuthorizeAsync(User, existingAd, new IsEligibleForAdvertisementDeleteRequirement());

            if (!result.Succeeded)
            {
                return Forbid();
            }

            await Mediator.Send(request);

            return NoContent();
        }

        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateAdvertisementCommand request)
        {
            var existingAd = await Mediator.Send(new ReadAdvertisementQuery { Id = id });

            var result = await _authorizationService.AuthorizeAsync(User, existingAd, new IsEligibleForAdvertisementUpdateRequirement());

            if (!result.Succeeded)
            {
                return Forbid();
            }

            // Need To change to request/response model + Command model

            request.Id = id;

            var updatedAd = await Mediator.Send(request);

            return Ok(updatedAd);
        }
    }
}
