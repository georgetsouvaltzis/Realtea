using Microsoft.AspNetCore.Mvc;
using Realtea.Core.DTOs.Advertisement;
using Realtea.Core.Services;

namespace Realtea.App.Controllers.V1
{
    [Route("/api/v1/[controller]")]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;
        public AdvertisementsController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _advertisementService.GetAllAsync());
        }

        [HttpGet]
        public async Task<ActionResult> GetById([FromQuery] int id)
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
