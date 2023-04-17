using System;
using MediatR;
using Realtea.Core.Enums;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.Queries;
using Realtea.Core.Results.Advertisement;

namespace Realtea.Core.Handlers.Queries.Advertisement
{
    public class ReadAdvertisementQueryHandler : IRequestHandler<ReadAdvertisementQuery, AdvertisementResult>
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public ReadAdvertisementQueryHandler(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public async Task<AdvertisementResult> Handle(ReadAdvertisementQuery request, CancellationToken cancellationToken)
        {
            var existingAd = await _advertisementRepository.GetByIdAsync(request.Id);

            if (existingAd == null)
                throw new InvalidOperationException(nameof(existingAd));

            return new AdvertisementResult
            {
                Id = existingAd.Id,
                UserId = existingAd.UserId,
                Name = existingAd.Name,
                Description = existingAd.Description,
                AdvertisementType = existingAd.AdvertisementType,
                IsActive = existingAd.IsActive,
                DealType = existingAd.DealType,
                Location = existingAd.Location,
                Price = existingAd.Price,
                SquareMeter = existingAd.SquareMeter,
            };
        }

        //private DealTypeEnum MapDealType(DealType dealType)
        //{
        //    return dealType switch
        //    {
        //        DealType.Mortgage => DealTypeEnum.Mortgage,
        //        DealType.Sale => DealTypeEnum.Sale,
        //        DealType.Rental => DealTypeEnum.Rental,
        //    };
        //}

        //private LocationEnum MapLocation(Location location)
        //{
        //    return location switch
        //    {
        //        Location.Tbilisi => LocationEnum.Tbilisi,
        //        Location.Batumi => LocationEnum.Batumi,
        //        Location.Kutaisi => LocationEnum.Kutaisi,
        //    };
        //}
    }
}

