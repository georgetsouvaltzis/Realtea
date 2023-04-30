using MediatR;
using Microsoft.AspNetCore.Mvc;
using Realtea.App.HttpContextWrapper;

namespace Realtea.App.Controllers.V1
{
    /// <summary>
    /// V1 Controller base which contains frequently-accessed information
    /// AND common route for inherited controllers.
    /// </summary>
    [Route("/api/v1/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public abstract class V1ControllerBase : ControllerBase
	{
        private readonly IHttpContextAccessorWrapper _wrapper;
        public V1ControllerBase(IMediator mediator, IHttpContextAccessorWrapper wrapper) 
        {
            Mediator = mediator;
            _wrapper = wrapper;
        }

        protected IMediator Mediator { get; }

        protected int CurrentUserId => _wrapper.GetUserId();
	}
}

