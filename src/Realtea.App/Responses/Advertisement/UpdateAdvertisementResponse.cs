using Realtea.App.Enums;

namespace Realtea.App.Responses.Advertisement
{
    public class UpdateAdvertisementResponse
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        // DO NOT FORGET TO CHANGE IT.
        public AdvertisementTypeEnum? AdvertisementType { get; set; }

        public DealTypeEnum? DealType { get; set; }

        public decimal? Price { get; set; }

        public decimal? SquareMeter { get; set; }

        public LocationEnum? Location { get; set; }

        public bool? IsActive { get; set; }
    }
}

