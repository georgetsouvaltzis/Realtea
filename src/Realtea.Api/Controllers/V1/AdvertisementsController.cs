using System.Net;
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
using Realtea.Core.Results.Advertisement;
using Swashbuckle.AspNetCore.Filters;

namespace Realtea.App.Controllers.V1
{
    /// <summary>
    /// Advertisement related-actions controller.
    /// </summary>
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

        /// <summary>
        /// Retrieves all Advertisements information.
        /// </summary>
        /// <param name="request">data for filtering.</param>
        /// <returns>Filtered advertisements.</returns>
        [HttpGet]
        [CacheResponse]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(IEnumerable<ReadAdvertisementsResponse>))]
        public async Task<ActionResult> GetAll([FromQuery] ReadFilteredAdvertisementRequest request)
        {
            var query = _mapper.Map<ReadFilteredAdvertisementsQuery>(request);

            var result = await Mediator.Send(query);

            var response = result.Select(x => _mapper.Map<ReadAdvertisementsResponse>(x));

            return Ok(response);
        }

        /// <summary>
        /// Advertisement retrieval by ID.
        /// </summary>
        /// <param name="request">ID of the Advertisement to retrieve.</param>
        /// <returns>Found advertisement.</returns>
        [HttpGet]
        [CacheResponse]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(ReadAdvertisementsResponse))]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Route("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] ReadAdvertisementRequest request)
        {
            var command = new ReadAdvertisementQuery { Id = request.Id };
            var queryResult = await Mediator.Send(command);

            var response = _mapper.Map<ReadAdvertisementsResponse>(queryResult);

            return Ok(response);

        }

        /// <summary>
        /// Creates new advertisement.
        /// </summary>
        /// <param name="request">Contains all the neccessary data for Advertisement.</param>
        /// <returns>Successful response with Location.</returns>
        [HttpPost]
        [BearerAuthorize]
        [SwaggerRequestExample(typeof(CreateAdvertisementRequestExample), typeof(CreateAdvertisementRequest))]
        [ProducesResponseType((int) HttpStatusCode.Created, Type = typeof(CreateAdvertisementResult))]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Add([FromBody] CreateAdvertisementRequest request)
        {
            var command = _mapper.Map<CreateAdvertisementCommand>(request);
            command.UserId = CurrentUserId;

            var response = await Mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, new ReadAdvertisementRequest { Id = response.Id });
        }

        /// <summary>
        /// Invalidates Advertisement.
        /// </summary>
        /// <param name="request">ID of the advertisement to invalidate.</param>
        /// <returns>Successful response</returns>
        [HttpDelete]
        [BearerAuthorize]
        [ProducesResponseType((int) HttpStatusCode.NoContent, Type = typeof(void))]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
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

        /// <summary>
        /// Updates advertisement
        /// </summary>
        /// <param name="id">ID of the advertisement to update.</param>
        /// <param name="request">Information required for update.</param>
        /// <returns>Successful response with Updated resource</returns>
        [HttpPut]
        [BearerAuthorize]
        [Route("{id:int}")]
        [SwaggerRequestExample(typeof(EditAdvertisementRequestExample), typeof(UpdateAdvertisementRequest))]
        [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(UpdateAdvertisementResponse))]
        [ProducesResponseType((int) HttpStatusCode.NotFound, Type = typeof(void))]
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
