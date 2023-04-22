using Realtea.App.Enums;

namespace Realtea.App.Requests.Advertisement
{
    public class CreateAdvertisementRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public AdvertisementTypeEnum AdvertisementType { get; set; }

        public DealTypeEnum DealType { get; set; }

        public decimal Price { get; set; }

        public decimal SquareMeter { get; set; }

        public LocationEnum Location { get; set; }

        public bool IsActive { get; set; }
    }
}

