using MediatR;
using Realtea.Core.Responses.Advertisement;

namespace Realtea.Core.Queries
{
    public class ReadFilteredAdvertisementsQuery : IRequest<IEnumerable<ReadAdvertisementsResponse>>
    {
        public DealTypeEnum? DealType { get; set; }
        public LocationEnum? Location { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public decimal? SqFrom { get; set; }
        public decimal? SqTo { get; set; }
    }
}

