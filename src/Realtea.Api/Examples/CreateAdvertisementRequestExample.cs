using System;
using Realtea.App.Enums;
using Realtea.App.Requests.Advertisement;
using Swashbuckle.AspNetCore.Filters;

namespace Realtea.Api.Examples
{
    public class CreateAdvertisementRequestExample : IExamplesProvider<CreateAdvertisementRequest>
    {
        public CreateAdvertisementRequest GetExamples()
        {
            return new CreateAdvertisementRequest
            {
                Name = "amazing flat",
                Description = "For sale",
                AdvertisementType = AdvertisementTypeEnum.Free,
                DealType = DealTypeEnum.Sale,
                Price = 30000m,
                Location = LocationEnum.Tbilisi,
                SquareMeter = 300m
            };
        }
    }
}

