using Realtea.App.Enums;

namespace Realtea.App.Requests.Advertisement
{
    /// <summary>
    /// Request which contains data for filtering to-be retrieved advertisements.
    /// </summary>
    public class ReadFilteredAdvertisementRequest
	{
        /// <summary>
        /// Starting price.
        /// </summary>
        public decimal? PriceFrom { get; set; }

        /// <summary>
        /// Ending price.
        /// </summary>
        public decimal? PriceTo { get; set; }

        /// <summary>
        /// Starting sq2.
        /// </summary>
        public decimal? SqFrom { get; set; }

        /// <summary>
        /// Ending sq2.
        /// </summary>
        public decimal? SqTo { get; set; }

        /// <summary>
        /// Filter advertisements with specific Deal type.
        /// </summary>
        public DealTypeEnum? DealType { get; set; }

        /// <summary>
        /// Filter advertisements with specific location.
        /// </summary>
        public LocationEnum? Location { get; set; }
    }
}

