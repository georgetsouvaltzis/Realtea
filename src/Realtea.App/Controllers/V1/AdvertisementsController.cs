using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realtea.App.Authorization;
using Realtea.App.Models;
using Realtea.Core.DTOs.Advertisement;
using Realtea.Core.Models;
using Realtea.Core.Services;

namespace Realtea.App.Controllers.V1
{
    public class AdvertisementsController : V1ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IAuthorizationService _authorizationService;

        public AdvertisementsController(IAdvertisementService advertisementService,
            IAuthorizationService authorizationService)
        {
            _advertisementService = advertisementService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] AdvertisementParams advertisementParams)
        {
            var context = HttpContext.User;
            var advertisementDescription = new AdvertisementDescription
            {
                DealType = advertisementParams.DealType,
                Location = advertisementParams.Location,
                PriceFrom = advertisementParams.PriceFrom,
                PriceTo = advertisementParams.PriceTo,
                SqFrom = advertisementParams.SqFrom,
                SqTo = advertisementParams.SqTo
            };
            return Ok(await _advertisementService.GetAllAsync(advertisementDescription));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            return Ok(await _advertisementService.GetByIdAsync(id));
        }

        [HttpPost]
        [Authorize]
        // currently it uses no authorized value of ID. Need to refactor it.
        public async Task<ActionResult> Add([FromBody] CreateAdvertisementDto createAdvertisementDto)
        {
            // Determine Whether is user authorized or not to Add Any extra ads.
            var userId = Convert.ToInt32(User.FindFirstValue("sub"));
            var result = await _advertisementService.AddAsync(createAdvertisementDto, userId);
            return CreatedAtAction(nameof(GetById), new {id = result }, result);
        }

        [HttpDelete]
        [Authorize]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingAd = await _advertisementService.GetByIdAsync(id);

            var result = await _authorizationService.AuthorizeAsync(User, existingAd, new IsEligibleForAdvertisementDeleteRequirement());

            if (!result.Succeeded)
            {
                return Forbid();
            }

            var userId = Convert.ToInt32(User.FindFirstValue("sub"));
            await _advertisementService.InvalidateAsync(id);

            return NoContent();
        }

        [HttpPut]
        [Authorize]
        [Route("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateAdvertisementDto dto)
        {
            var existingAd = await _advertisementService.GetByIdAsync(id);

            var result = await _authorizationService.AuthorizeAsync(User, existingAd, new IsEligibleForAdvertisementUpdateRequirement());

            if (!result.Succeeded)
            {
                return Forbid();
            }

            var userId = Convert.ToInt32(User.FindFirstValue("sub"));
            var updatedAd = await _advertisementService.UpdateAsync(id, userId, dto);

            return Ok(updatedAd);
        }
    }
}
