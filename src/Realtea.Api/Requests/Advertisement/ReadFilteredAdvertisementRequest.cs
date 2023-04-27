using Realtea.App.Enums;

namespace Realtea.App.Requests.Advertisement
{
    public class ReadFilteredAdvertisementRequest
	{
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public decimal? SqFrom { get; set; }
        public decimal? SqTo { get; set; }
        public DealTypeEnum? DealType { get; set; }
        public LocationEnum? Location { get; set; }
    }
}

