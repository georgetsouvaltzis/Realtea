using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realtea.App.Authorization;
using Realtea.App.Models;
using Realtea.Core.Interfaces;
using Realtea.Core.Models;
using Realtea.Core.Enums;
using Realtea.Core.Services;
using Realtea.App.Enums;
using MediatR;
using Realtea.Core.Queries;
using Realtea.App.HttpContextWrapper;
using Realtea.Core.Commands.Advertisement;
using Realtea.Core.Requests;

namespace Realtea.App.Controllers.V1
{
    public class AdvertisementsController : V1ControllerBase
    {
        private readonly IMediator _mediator;
        //private readonly IAdvertisementService _advertisementService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessorWrapper _httpContextAccessorWrapper;

        public AdvertisementsController(
            IAuthorizationService authorizationService,
            IMediator mediator,
            IHttpContextAccessorWrapper httpContextAccessorWrapper)
        {
            _mediator = mediator;
            _httpContextAccessorWrapper = httpContextAccessorWrapper;
        }

        // should update Based on IsActive, Desc/asc, etc.
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] ReadFilteredAdvertisementsQuery request)
        {
            var result = await _mediator.Send(request);

            //var advertisementDescription = new AdvertisementDescription
            //{
            //    Location = MapLocation(request.Location),
            //    DealType = MapDealType(request.DealType),
            //    PriceFrom = request.PriceFrom,
            //    PriceTo = request.PriceTo,
            //    SqFrom = request.SqFrom,
            //    SqTo = request.SqTo
            //};

            return Ok(result);

            //var result = await _advertisementService.GetAllAsync(advertisementDescription);


            //return Ok(await _advertisementService.GetAllAsync(advertisementDescription));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] ReadAdvertisementQuery request)
        {

            var result = await _mediator.Send(request);

            //return Ok(await _advertisementService.GetByIdAsync(id));
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
            // Determine Whether is user authorized or not to Add Any extra ads.
            //var userId = Convert.ToInt32(User.FindFirstValue("sub"));

            //return RedirectToAction("MakePayment", "Payments", new { advertisementId = 1 });

            var command = new CreateAdvertisementCommand
            {
                Name = request.Name,
                AdvertisementType = request.AdvertisementType,
                Description = request.Description,
                IsActive = request.IsActive,
                UserId = userId,
                // TODO: Need to change this.S
                UpdateAdvertisementDetails = request.UpdateAdvertisementDetails,
            };

            var resultt = await _mediator.Send(command);

            //var result = await _advertisementService.AddAsync(createAdvertisementDto, userId);

            return CreatedAtAction(nameof(GetById), new { id = resultt.Id }, new ReadAdvertisementQuery { Id = resultt.Id });
        }

        [HttpDelete]
        [Authorize]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] DeleteAdvertisementCommand request)
        {
            var existingAd = await _mediator.Send(new ReadAdvertisementQuery { Id = request.Id });

            //var existingAd = await _advertisementService.GetByIdAsync(id);

            // Need to move it to Deletecommandhandler.
            var result = await _authorizationService.AuthorizeAsync(User, existingAd, new IsEligibleForAdvertisementDeleteRequirement());

            if (!result.Succeeded)
            {
                return Forbid();
            }

            await _mediator.Send(request);
            //var userId = Convert.ToInt32(User.FindFirstValue("sub"));
            //await _advertisementService.InvalidateAsync(id);

            return NoContent();
        }

        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateAdvertisementCommand request)
        {
            //var existingAd = await _advertisementService.GetByIdAsync(id);

            var existingAd = await _mediator.Send(new ReadAdvertisementQuery { Id = id });

            var result = await _authorizationService.AuthorizeAsync(User, existingAd, new IsEligibleForAdvertisementUpdateRequirement());

            if (!result.Succeeded)
            {
                return Forbid();
            }

            //var userId = Convert.ToInt32(User.FindFirstValue("sub"));
            var userId = _httpContextAccessorWrapper.GetUserId();

            var updatedAd = await _mediator.Send(request);
            //var updatedAd = await _advertisementService.UpdateAsync(id, userId, dto);

            return Ok(updatedAd);
        }
    }
}
