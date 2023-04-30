using Realtea.App.Enums;
using Realtea.App.JsonConverters;
using System.Text.Json.Serialization;

namespace Realtea.App.Requests.Advertisement
{
    /// <summary>
    /// Incoming Advertisement request for creating new ad.
    /// </summary>
    public class CreateAdvertisementRequest
    {
        /// <summary>
        /// Name of the advertisement.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the advertisement.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Advertisement type Which could be Free or Paid.
        /// </summary>
        [JsonConverter(typeof(EnumConverter<AdvertisementTypeEnum>))]
        public AdvertisementTypeEnum AdvertisementType { get; set; }

        /// <summary>
        /// Deal type of the Advertisement. Whether it's For Sale, Rent etc.
        /// </summary>
        [JsonConverter(typeof(EnumConverter<DealTypeEnum>))]
        public DealTypeEnum DealType { get; set; }

        /// <summary>
        /// Price user is asking for.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Square meter of the property that user is enlisting.
        /// </summary>
        public decimal SquareMeter { get; set; }

        /// <summary>
        /// Location where property is located.
        /// </summary>
        [JsonConverter(typeof(EnumConverter<LocationEnum>))]
        public LocationEnum Location { get; set; }

        /// <summary>
        /// Determines whether user wants to enlist it on Advertisements.
        /// </summary>
        public bool IsActive { get; set; }
    }
}

