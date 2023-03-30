using Realtea.Domain.Enums;

namespace Realtea.Core.DTOs.Advertisement
{
    public class ReadAdvertisementDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public AdvertisementType AdvertisementType { get; set; }

        public ReadAdvertisementDetailsDto ReadAdvertisementDetailsDto { get; set; }
    }
}
