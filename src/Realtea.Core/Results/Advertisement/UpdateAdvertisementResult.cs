using Realtea.Core.Enums;

namespace Realtea.Core.Results.Advertisement
{
    public class UpdateAdvertisementResult
	{
        public string Name { get; set; }

        public string Description { get; set; }

        public AdvertisementType AdvertisementType { get; set; }

        public DealType DealType { get; set; }

        public Location Location { get; set; }

        public decimal SquareMeter { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }
	}
}

