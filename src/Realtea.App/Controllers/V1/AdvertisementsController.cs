using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realtea.App.Models;
using Realtea.Core.DTOs.Advertisement;
using Realtea.Core.Models;
using Realtea.Core.Services;

namespace Realtea.App.Controllers.V1
{
    [Route("/api/v1/[controller]")]
    [Produces("application/json")]
    [Authorize]

    public class AdvertisementsController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;
        public AdvertisementsController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
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
        public async Task<ActionResult> Add([FromBody] CreateAdvertisementDto createAdvertisementDto)
        {
            var result = await _advertisementService.AddAsync(createAdvertisementDto);

            return CreatedAtRoute(nameof(GetById), result);
        }
    }
}
