using System;
using MediatR;
using Realtea.Core.Commands.Advertisement;
using Realtea.Core.Enums;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.Responses.Advertisement;

namespace Realtea.Core.Handlers.Commands.Advertisement
{
	public class UpdateAdvertisementCommandHandler : IRequestHandler<UpdateAdvertisementCommand, UpdateAdvertisementResponse>
	{
        private readonly IAdvertisementRepository _advertisementRepository;
		public UpdateAdvertisementCommandHandler(IAdvertisementRepository advertisementRepository)
		{
            _advertisementRepository = advertisementRepository;

		}

        public async Task<UpdateAdvertisementResponse> Handle(UpdateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var existingAd = await _advertisementRepository.GetByIdAsync(request.Id);

            if (existingAd == null)
                throw new InvalidOperationException(nameof(existingAd));


            existingAd.Name = request.Name ?? existingAd.Name;
            existingAd.Description = request.Description ?? existingAd.Description;
            existingAd.IsActive = request.IsActive ?? existingAd.IsActive;

            if (request.UpdateAdvertisementDetails != null)
            {
                // TODO: DO NOT FORGET ABOUT THIS
                //existingAd.AdvertisementDetails.DealType = request.UpdateAdvertisementDetails.DealType ?? existingAd.AdvertisementDetails.DealType;
                //existingAd.AdvertisementDetails.Location = request.UpdateAdvertisementDetails.Location ?? existingAd.AdvertisementDetails.Location;
                existingAd.AdvertisementDetails.Price = request.UpdateAdvertisementDetails.Price ?? existingAd.AdvertisementDetails.Price;
                existingAd.AdvertisementDetails.SquareMeter = request.UpdateAdvertisementDetails.Price ?? existingAd.AdvertisementDetails.Price;
            }


            var modifiedAd = await _advertisementRepository.UpdateAsync(existingAd);

            return new UpdateAdvertisementResponse
            {
                Name = modifiedAd.Name,
                Description = modifiedAd.Description,
                IsActive = modifiedAd.IsActive,
                AdvertisementType = modifiedAd.AdvertisementType,
                UpdateAdvertisementDetails = new UpdateAdvertisementDetailsResponse
                {
                    DealType = MapDealType(modifiedAd.AdvertisementDetails.DealType),
                    Location = MapLocation(modifiedAd.AdvertisementDetails.Location),
                    SquareMeter = modifiedAd.AdvertisementDetails.SquareMeter,
                    Price = modifiedAd.AdvertisementDetails.Price,
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

