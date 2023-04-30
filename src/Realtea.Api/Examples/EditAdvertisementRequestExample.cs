using System;
using Realtea.App.Requests.Advertisement;
using Swashbuckle.AspNetCore.Filters;

namespace Realtea.Api.Examples
{
    public class EditAdvertisementRequestExample : IExamplesProvider<UpdateAdvertisementRequest>
    {
        public UpdateAdvertisementRequest GetExamples()
        {
            return new UpdateAdvertisementRequest
            {
                Name = "Another super flat"
            };
        }
    }
}

