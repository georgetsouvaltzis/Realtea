using System;
//using Realtea.Core.DTOs.Advertisement;
using Realtea.Core.Enums;

namespace Realtea.Core.Responses.Advertisement
{
    public class ReadAdvertisementsResponse
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public AdvertisementType AdvertisementType { get; set; }

        public bool IsActive { get; set; }

        public ReadAdvertisementDetailResponse ReadAdvertisementDetail{ get; set; }
    }

    public class ReadAdvertisementDetailResponse
    {
        public int Id { get; set; }

        public DealTypeEnum DealType { get; set; }

        public decimal Price { get; set; }

        public decimal SquareMeter { get; set; }

        public LocationEnum Location { get; set; }

    }

  
	public class ReadAdvertisementsParams
	{
        public DealTypeEnum? DealType { get; set; }
        public LocationEnum? Location { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public decimal? SqFrom { get; set; }
        public decimal? SqTo { get; set; }
    }

    public enum DealTypeEnum
    {
        Sale = 0,
        Mortgage = 1,
        Rental = 2,
    }

    public enum LocationEnum
    {
        Tbilisi = 0,
        Batumi = 1,
        Kutaisi = 2,
    }
}

