using Realtea.Domain.Enums;

namespace Realtea.Core.Models
{
    public class AdvertisementDescription
    {
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public decimal? SqFrom { get; set; }
        public decimal? SqTo { get; set; }
        public Location? Location { get; set; }
        public DealType? DealType { get; set; }
    }
}
