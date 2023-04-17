using MediatR;
using Realtea.Core.Enums;
using Realtea.Core.Results.Advertisement;

namespace Realtea.Core.Queries
{
    public class ReadFilteredAdvertisementsQuery : IRequest<IEnumerable<AdvertisementResult>>
    {
        public DealType? DealType { get; set; }
        public Location? Location { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public decimal? SqFrom { get; set; }
        public decimal? SqTo { get; set; }
    }
}

