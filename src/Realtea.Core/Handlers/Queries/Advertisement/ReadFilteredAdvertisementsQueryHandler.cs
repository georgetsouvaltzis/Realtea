using System;
using AutoMapper;
using MediatR;
using Realtea.Core.Enums;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.Models;
using Realtea.Core.Queries;
using Realtea.Core.Results.Advertisement;

namespace Realtea.Core.Handlers.Queries.Advertisement
{
    public class ReadFukteredAdvertisementsQueryHandler : IRequestHandler<ReadFilteredAdvertisementsQuery, IEnumerable<AdvertisementResult>>
    {
        private readonly IMapper _mapper;
        private readonly IAdvertisementRepository _advertisementRepository;
        public ReadFukteredAdvertisementsQueryHandler(IAdvertisementRepository advertisementRepository, IMapper mapper)
        {
            _advertisementRepository = advertisementRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AdvertisementResult>> Handle(ReadFilteredAdvertisementsQuery request, CancellationToken cancellationToken)
        {
            var queryable = _advertisementRepository.GetAsQueryable();

            if (request.DealType.HasValue)
                queryable = queryable.Where(x => x.DealType == request.DealType.Value);

            if (request.PriceFrom.HasValue)
                queryable = queryable.Where(x => x.Price >= request.PriceFrom);

            if (request.PriceTo.HasValue)
                queryable = queryable.Where(x => x.Price <= request.PriceTo);

            if (request.SqFrom.HasValue)
                queryable = queryable.Where(x => x.SquareMeter >= request.SqFrom);

            if (request.SqTo.HasValue)
                queryable = queryable.Where(x => x.SquareMeter <= request.SqTo);

            if (request.Location.HasValue)
                queryable = queryable.Where(x => x.Location == request.Location.Value);

            var result = queryable.ToList();

            return result.Select(x => _mapper.Map<AdvertisementResult>(x));
        }
    }
}

