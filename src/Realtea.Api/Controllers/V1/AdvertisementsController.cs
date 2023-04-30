using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realtea.Api;
using Realtea.Api.Attributes;
using Realtea.Api.Examples;
using Realtea.App.Filters;
using Realtea.App.HttpContextWrapper;
using Realtea.App.Identity.Authorization.Requirements.Advertisement;
using Realtea.App.Requests.Advertisement;
using Realtea.App.Responses.Advertisement;
using Realtea.Core.Commands.Advertisement;
using Realtea.Core.Queries;
using Swashbuckle.AspNetCore.Filters;

namespace Realtea.App.Controllers.V1
{
    public class AdvertisementsController : V1ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public AdvertisementsController(IMediator mediator,
            IAuthorizationService authorizationService,
            IMapper mapper,
            IHttpContextAccessorWrapper wrapper) : base(mediator, wrapper)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        [HttpGet]
        [CacheResponse]
        public async Task<ActionResult> GetAll([FromQuery] ReadFilteredAdvertisementRequest request)
        {
            var query = _mapper.Map<ReadFilteredAdvertisementsQuery>(request);

            var result = await Mediator.Send(query);

            var response = result.Select(x => _mapper.Map<ReadAdvertisementsResponse>(x));

            return Ok(response);
        }

        [HttpGet]
        [Route("{id:int}")]
        [CacheResponse]
        public async Task<ActionResult> GetById([FromRoute] ReadAdvertisementRequest request)
        {
            var command = new ReadAdvertisementQuery { Id = request.Id };
            var queryResult = await Mediator.Send(command);

            var response = _mapper.Map<ReadAdvertisementsResponse>(queryResult);

            return Ok(response);

        }

        [HttpPost]
        [BearerAuthorize]
        [SwaggerRequestExample(typeof(CreateAdvertisementRequestExample), typeof(CreateAdvertisementRequest))]
        public async Task<ActionResult> Add([FromBody] CreateAdvertisementRequest request)
        {
            var command = _mapper.Map<CreateAdvertisementCommand>(request);
            command.UserId = CurrentUserId;

            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, new ReadAdvertisementRequest { Id = response.Id });
        }

        [HttpDelete]
        [BearerAuthorize]
        [Route("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] DeleteAdvertisementRequest request)
        {
            var existingAd = await Mediator.Send(new ReadAdvertisementQuery { Id = request.Id });

            var result = await _authorizationService.AuthorizeAsync(User, existingAd, new IsEligibleForAdvertisementDeleteRequirement());

            if (!result.Succeeded)
            {
                return Forbid();
            }

            var command = new DeleteAdvertisementCommand { Id = CurrentUserId };

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        [BearerAuthorize]
        [Route("{id:int}")]
        [SwaggerRequestExample(typeof(EditAdvertisementRequestExample), typeof(UpdateAdvertisementRequest))]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateAdvertisementRequest request)
        {
            var existingAd = await Mediator.Send(new ReadAdvertisementQuery { Id = id });

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, existingAd, new IsEligibleForAdvertisementUpdateRequirement());

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            var command = _mapper.Map<UpdateAdvertisementCommand>(request);
            command.Id = id;

            var result = await Mediator.Send(command);

            var response = _mapper.Map<UpdateAdvertisementResponse>(result);

            return Ok(response);
        }
    }
}
