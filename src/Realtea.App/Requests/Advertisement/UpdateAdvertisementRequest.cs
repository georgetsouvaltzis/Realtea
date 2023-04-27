using Realtea.App.Enums;
using Realtea.App.JsonConverters;
using System.Text.Json.Serialization;

namespace Realtea.App.Requests.Advertisement
{
    public class UpdateAdvertisementRequest
	{
        public string? Name { get; set; }

        public string? Description { get; set; }

        [JsonConverter(typeof(EnumConverter<DealTypeEnum>))]
        public DealTypeEnum? DealType { get; set; }

        [JsonConverter(typeof(EnumConverter<LocationEnum>))]
        public LocationEnum? Location { get; set; }

        [JsonConverter(typeof(EnumConverter<AdvertisementTypeEnum>))]
        public AdvertisementTypeEnum? AdvertisementType { get; set; }

        public decimal? Price { get; set; }

        public decimal? SquareMeter { get; set; }

        public bool? IsActive { get; set; }
	}
}

