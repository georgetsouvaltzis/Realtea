using Realtea.Domain.Enums;

namespace Realtea.Core.DTOs.Advertisement
{
    public class ReadAdvertisementDetailsDto
    {
        public int Id { get; set; }

        public DealType DealType { get; set; }

        public decimal Price { get; set; }

        public decimal SquareMeter { get; set; }

        public Location Location { get; set; }
    }
}
