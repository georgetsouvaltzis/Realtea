using System;
using MediatR;
using Realtea.Core.Enums;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.Queries;
using Realtea.Core.Responses.Advertisement;

namespace Realtea.Core.Handlers.Queries.Advertisement
{
    public class ReadAdvertisementQueryHandler : IRequestHandler<ReadAdvertisementQuery, ReadAdvertisementsResponse>
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public ReadAdvertisementQueryHandler(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public async Task<ReadAdvertisementsResponse> Handle(ReadAdvertisementQuery request, CancellationToken cancellationToken)
        {
            var existingAd = await _advertisementRepository.GetByIdAsync(request.Id);

            if (existingAd == null)
                throw new InvalidOperationException(nameof(existingAd));

            return new ReadAdvertisementsResponse
            {
                Id = existingAd.Id,
                UserId = existingAd.UserId,
                Name = existingAd.Name,
                Description = existingAd.Description,
                AdvertisementType = existingAd.AdvertisementType,
                IsActive = existingAd.IsActive,
                ReadAdvertisementDetail = new ReadAdvertisementDetailResponse
                {
                    Id = existingAd.AdvertisementDetails.Id,
                    DealType = MapDealType(existingAd.AdvertisementDetails.DealType),
                    Location = MapLocation(existingAd.AdvertisementDetails.Location),
                    Price = existingAd.AdvertisementDetails.Price,
                    SquareMeter = existingAd.AdvertisementDetails.SquareMeter,
                }
            };
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

