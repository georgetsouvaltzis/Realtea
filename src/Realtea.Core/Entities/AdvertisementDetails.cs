using System;
using Realtea.Core.Enums;

namespace Realtea.Core.Entities
{
	public class AdvertisementDetails : BaseEntity
	{
        public Advertisement Advertisement { get; set; }

        public int AdvertisementId { get; set; }

        public DealType DealType { get; set; }

        public decimal Price { get; set; }

        public decimal SquareMeter { get; set; }

        public Location Location { get; set; }
    }
}

