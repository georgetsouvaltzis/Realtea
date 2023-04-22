using AutoMapper;
using MediatR;
using Realtea.Core.Commands.Advertisement;
using Realtea.Core.Enums;
using Realtea.Core.Exceptions;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.Results.Advertisement;

namespace Realtea.Core.Handlers.Commands.Advertisement
{
    public class UpdateAdvertisementCommandHandler : IRequestHandler<UpdateAdvertisementCommand, UpdateAdvertisementResult>
	{
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IMapper _mapper;
		public UpdateAdvertisementCommandHandler(IAdvertisementRepository advertisementRepository, IMapper mapper)
		{
            _advertisementRepository = advertisementRepository;
            _mapper = mapper;
		}

        public async Task<UpdateAdvertisementResult> Handle(UpdateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var existingAd = await _advertisementRepository.GetByIdAsync(request.Id);

            if (existingAd == null)
                throw new ApiException($"Advertisement with ID: {request.Id} does not exist.", FailureType.Absent);


            existingAd.Name = request.Name ?? existingAd.Name;
            existingAd.Description = request.Description ?? existingAd.Description;
            existingAd.IsActive = request.IsActive ?? existingAd.IsActive;


            if(request.DealType != null)
            {
                existingAd.DealType = request.DealType.Value;
            }

            if(request.Location != null)
            {
                existingAd.Location = request.Location.Value;
            }

            if(request.Price != null)
            {
                existingAd.Price = request.Price.Value;
            }

            if(request.SquareMeter != null)
            {
                existingAd.SquareMeter = request.SquareMeter.Value;
            }

            var modifiedAd = await _advertisementRepository.UpdateAsync(existingAd);

            var mapped = _mapper.Map<UpdateAdvertisementResult>(modifiedAd);

            return mapped;
            //return new UpdateAdvertisementResult
            //{
            //    Name = modifiedAd.Name,
            //    Description = modifiedAd.Description,
            //    IsActive = modifiedAd.IsActive,
            //    AdvertisementType = modifiedAd.AdvertisementType,
            //    DealType = modifiedAd.DealType,
            //    Location = modifiedAd.Location,
            //    SquareMeter = modifiedAd.SquareMeter,
            //    Price = modifiedAd.Price,
            //};
        }
    }
}

