using Realtea.Domain.Enums;

namespace Realtea.Core.DTOs.Advertisement
{
    public class CreateAdvertisementDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public AdvertisementType AdvertisementType { get; set; }

        public CreateAdvertisementDetailsDto CreateAdvertisementDetailsDto { get; set; }
    }
}
