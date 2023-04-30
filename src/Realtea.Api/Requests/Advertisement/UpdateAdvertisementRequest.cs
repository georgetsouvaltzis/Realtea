using Realtea.App.Enums;
using Realtea.App.JsonConverters;
using System.Text.Json.Serialization;

namespace Realtea.App.Requests.Advertisement
{
    /// <summary>
    /// Incoming request for updating advertisement.
    /// </summary>
    public class UpdateAdvertisementRequest
	{
        /// <summary>
        /// Name of the advertisement.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Description of the advertisement.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Deal type.
        /// </summary>
        [JsonConverter(typeof(EnumConverter<DealTypeEnum>))]
        public DealTypeEnum? DealType { get; set; }

        /// <summary>
        /// Location type.
        /// </summary>
        [JsonConverter(typeof(EnumConverter<LocationEnum>))]
        public LocationEnum? Location { get; set; }

        /// <summary>
        /// Advertiseemnt type.
        /// </summary>
        [JsonConverter(typeof(EnumConverter<AdvertisementTypeEnum>))]
        public AdvertisementTypeEnum? AdvertisementType { get; set; }

        /// <summary>
        /// Price.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Square meter.
        /// </summary>
        public decimal? SquareMeter { get; set; }

        /// <summary>
        /// Whether advertisement is active or not.
        /// </summary>
        public bool? IsActive { get; set; }
	}
}

