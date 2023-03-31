using System;
using Microsoft.AspNetCore.Mvc;

namespace Realtea.App.Controllers.V1
{
    [Route("/api/v1/[controller]")]
    [Produces("application/json")]
    public abstract class V1ControllerBase : ControllerBase
	{
	}
}

