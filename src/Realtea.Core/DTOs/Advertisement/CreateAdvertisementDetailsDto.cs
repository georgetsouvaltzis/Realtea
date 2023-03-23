using Realtea.Domain.Enums;

namespace Realtea.Core.DTOs.Advertisement
{
    public class CreateAdvertisementDetailsDto
    {
        public DealType DealType { get; set; }

        public decimal Price { get; set; }

        public decimal SquareMeter { get; set; }

        public Location Location { get; set; }
    }
}
