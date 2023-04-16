using System;
using MediatR;
using Realtea.Core.Enums;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.Models;
using Realtea.Core.Queries;
using Realtea.Core.Responses;
using Realtea.Core.Responses.Advertisement;

namespace Realtea.Core.Handlers.Queries.Advertisement
{
    public class ReadFukteredAdvertisementsQueryHandler : IRequestHandler<ReadFilteredAdvertisementsQuery, IEnumerable<ReadAdvertisementsResponse>>
    {

        private readonly IAdvertisementRepository _advertisementRepository;
        public ReadFukteredAdvertisementsQueryHandler(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public async Task<IEnumerable<ReadAdvertisementsResponse>> Handle(ReadFilteredAdvertisementsQuery request, CancellationToken cancellationToken)
        {
            var f = _advertisementRepository.GetAsQueryable();

            //if (request.DealType.HasValue)
            //    f = f.Where(x => x.AdvertisementDetails.DealType == request.DealType.Value);

            if (request.PriceFrom.HasValue)
                f = f.Where(x => x.AdvertisementDetails.Price >= request.PriceFrom);

            if (request.PriceTo.HasValue)
                f = f.Where(x => x.AdvertisementDetails.Price <= request.PriceTo);

            if (request.SqFrom.HasValue)
                f = f.Where(x => x.AdvertisementDetails.SquareMeter >= request.SqFrom);

            if (request.SqTo.HasValue)
                f = f.Where(x => x.AdvertisementDetails.SquareMeter <= request.SqTo);

            //if (request.Location.HasValue)
            //    f = f.Where(x => x.AdvertisementDetails.Location == request.Location.Value);

            var asdf = f.ToList();

            return asdf.Select(x => new ReadAdvertisementsResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                AdvertisementType = x.AdvertisementType,
                ReadAdvertisementDetail = new ReadAdvertisementDetailResponse
                {
                    Id = x.AdvertisementDetails.Id,
                    DealType = MapDealType(x.AdvertisementDetails.DealType),
                    Location = MapLocation(x.AdvertisementDetails.Location),
                    Price = x.AdvertisementDetails.Price,
                    SquareMeter = x.AdvertisementDetails.SquareMeter,
                }
            });
        }


        private DealTypeEnum MapDealType(DealType dealType)
        {
            return dealType switch
            {
                DealType.Mortgage => DealTypeEnum.Mortgage,
                DealType.Sale => DealTypeEnum.Sale,
                DealType.Rental => DealTypeEnum.Rental,
            };
        }

        private LocationEnum MapLocation(Location location)
        {
            return location switch
            {
                Location.Tbilisi => LocationEnum.Tbilisi,
                Location.Batumi => LocationEnum.Batumi,
                Location.Kutaisi => LocationEnum.Kutaisi,
            };
        }
    }
}

