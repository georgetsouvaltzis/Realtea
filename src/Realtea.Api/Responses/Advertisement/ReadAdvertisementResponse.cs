using Realtea.App.Enums;
using Realtea.App.JsonConverters;
using System.Text.Json.Serialization;

namespace Realtea.App.Responses.Advertisement
{
    public class ReadAdvertisementsResponse
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(EnumConverter<AdvertisementTypeEnum>))]
        public AdvertisementTypeEnum AdvertisementType { get; set; }

        public bool IsActive { get; set; }

        [JsonConverter(typeof(EnumConverter<DealTypeEnum>))]
        public DealTypeEnum DealType { get; set; }

        public decimal Price { get; set; }

        public decimal SquareMeter { get; set; }

        [JsonConverter(typeof(EnumConverter<LocationEnum>))]
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
}

