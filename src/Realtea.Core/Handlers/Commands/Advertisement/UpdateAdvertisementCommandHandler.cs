using AutoMapper;
using MediatR;
using Realtea.Core.Commands.Advertisement;
using Realtea.Core.Enums;
using Realtea.Core.Exceptions;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.Results.Advertisement;
using Realtea.Core.ValueObjects;

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


            if (!string.IsNullOrEmpty(request.Name))
                existingAd.ChangeName(request.Name);

            if (!string.IsNullOrEmpty(request.Description))
                existingAd.ChangeDescription(request.Description);

            if (request.IsActive.HasValue)
                existingAd.SetIsActive(request.IsActive.Value);

            if(request.DealType.HasValue)
                existingAd.ChangeDealType(request.DealType.Value);
            

            if(request.Location.HasValue)
                existingAd.ChangeLocation(request.Location.Value);

            if(request.Price.HasValue)
                existingAd.ChangePrice(Money.Create(request.Price.Value));

            if (request.SquareMeter.HasValue)
                existingAd.ChangeSq2(Sq2.Create(request.SquareMeter.Value));

            var modifiedAd = await _advertisementRepository.UpdateAsync(existingAd);

            var mapped = _mapper.Map<UpdateAdvertisementResult>(modifiedAd);

            return mapped;
        }
    }
}

