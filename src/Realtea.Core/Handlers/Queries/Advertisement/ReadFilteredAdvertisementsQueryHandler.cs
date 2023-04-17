using System;
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

        private readonly IAdvertisementRepository _advertisementRepository;
        public ReadFukteredAdvertisementsQueryHandler(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
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

            var asdf = queryable.ToList();

            return asdf.Select(x => new AdvertisementResult
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                AdvertisementType = x.AdvertisementType,
                DealType = x.DealType,
                Location = x.Location,
                Price = x.Price,
                SquareMeter = x.SquareMeter
            });
        }
    }
}

