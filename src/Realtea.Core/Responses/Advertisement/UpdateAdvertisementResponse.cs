using System;
using Realtea.Core.Enums;

namespace Realtea.Core.Responses.Advertisement
{
	public class UpdateAdvertisementResponse
	{
        public string? Name { get; set; }

        public string? Description { get; set; }

        // DO NOT FORGET TO CHANGE IT.
        public AdvertisementType? AdvertisementType { get; set; }

        public UpdateAdvertisementDetailsResponse? UpdateAdvertisementDetails { get; set; }

        public bool? IsActive { get; set; }
    }

    public class UpdateAdvertisementDetailsResponse
    {
        public DealTypeEnum? DealType { get; set; }

        public decimal? Price { get; set; }

        public decimal? SquareMeter { get; set; }

        public LocationEnum? Location { get; set; }
    }
}

