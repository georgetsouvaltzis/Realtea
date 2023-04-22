using AutoMapper;
using MediatR;
using Realtea.Core.Enums;
using Realtea.Core.Exceptions;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.Queries;
using Realtea.Core.Results.Advertisement;

namespace Realtea.Core.Handlers.Queries.Advertisement
{
    public class ReadAdvertisementQueryHandler : IRequestHandler<ReadAdvertisementQuery, AdvertisementResult>
    {
        private readonly IMapper _mapper;
        private readonly IAdvertisementRepository _advertisementRepository;

        public ReadAdvertisementQueryHandler(IAdvertisementRepository advertisementRepository, IMapper mapper)
        {
            _advertisementRepository = advertisementRepository;
            _mapper = mapper;
        }

        public async Task<AdvertisementResult> Handle(ReadAdvertisementQuery request, CancellationToken cancellationToken)
        {
            var existingAd = await _advertisementRepository.GetByIdAsync(request.Id);

            if (existingAd == null)
                throw new ApiException($"Advertisement with ID: {request.Id} does not exist.", FailureType.Absent);


            var mapped = _mapper.Map<AdvertisementResult>(existingAd);

            return mapped;
        }
    }
}

