using Realtea.Core.Enums;

namespace Realtea.App.Models
{
    public class AdvertisementParams
    {
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public decimal? SqFrom { get; set; }
        public decimal? SqTo { get; set; }
        public Location? Location { get; set; }
        public DealType? DealType { get; set; }
    }
}
