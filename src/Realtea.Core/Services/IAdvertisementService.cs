using Realtea.Core.DTOs.Advertisement;
using Realtea.Core.Models;
using Realtea.Domain.Entities;
using Realtea.Domain.Repositories;

namespace Realtea.Core.Services
{
    public interface IAdvertisementService
    {
        Task<int> AddAsync(CreateAdvertisementDto createAdvertisementDto);

        Task<ReadAdvertisementDto> GetByIdAsync(int id);

        Task<IEnumerable<ReadAdvertisementDto>> GetAllAsync(AdvertisementDescription advertisementDescription);
    }

    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public AdvertisementService(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public async Task<int> AddAsync(CreateAdvertisementDto createAdvertisementDto)
        {
            _ = createAdvertisementDto ?? throw new ArgumentNullException(nameof(createAdvertisementDto));
            _ = createAdvertisementDto.CreateAdvertisementDetailsDto ?? throw new ArgumentNullException(nameof(createAdvertisementDto.CreateAdvertisementDetailsDto));

            if (string.IsNullOrEmpty(createAdvertisementDto.Name))
            {
                throw new InvalidOperationException(nameof(createAdvertisementDto.Name));
            }

            if (string.IsNullOrEmpty(createAdvertisementDto.Description))
            {
                throw new InvalidOperationException(nameof(createAdvertisementDto.Description));
            }

            var newAdvertisement = new Advertisement
            {
                Name = createAdvertisementDto.Name,
                Description = createAdvertisementDto.Description,
                AdvertisementDetails = new AdvertisementDetails
                {
                    DealType = createAdvertisementDto.CreateAdvertisementDetailsDto.DealType,
                    Location = createAdvertisementDto.CreateAdvertisementDetailsDto.Location,
                    Price = createAdvertisementDto.CreateAdvertisementDetailsDto.Price,
                    SquareMeter = createAdvertisementDto.CreateAdvertisementDetailsDto.SquareMeter,
                }
            };

            await _advertisementRepository.AddAsync(newAdvertisement);

            return newAdvertisement.Id;
        }

        public async Task<IEnumerable<ReadAdvertisementDto>> GetAllAsync(AdvertisementDescription advertisementDescription)
        {
            var f = await _advertisementRepository.GetAllAsync();
            
            if (advertisementDescription.DealType != null)
                f = f.Where(x => x.AdvertisementDetails.DealType == advertisementDescription.DealType);

            if (advertisementDescription.PriceFrom.HasValue)
                f = f.Where(x => x.AdvertisementDetails.Price >= advertisementDescription.PriceFrom);

            if (advertisementDescription.PriceTo.HasValue)
                f = f.Where(x => x.AdvertisementDetails.Price <= advertisementDescription.PriceTo);

            if (advertisementDescription.SqFrom.HasValue)
                f = f.Where(x => x.AdvertisementDetails.SquareMeter >= advertisementDescription.SqFrom);

            if (advertisementDescription.SqTo.HasValue)
                f = f.Where(x => x.AdvertisementDetails.SquareMeter <= advertisementDescription.SqTo);

            if (advertisementDescription.Location.HasValue)
                f = f.Where(x => x.AdvertisementDetails.Location == advertisementDescription.Location);
            
            return f.Select(x => new ReadAdvertisementDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                AdvertisementType = x.AdvertisementType,
                ReadAdvertisementDetailsDto = new ReadAdvertisementDetailsDto
                {
                    Id = x.AdvertisementDetails.Id,
                    DealType = x.AdvertisementDetails.DealType,
                    Location = x.AdvertisementDetails.Location,
                    Price = x.AdvertisementDetails.Price,
                    SquareMeter = x.AdvertisementDetails.SquareMeter,
                }
            });

            //return (await _advertisementRepository.GetAllAsync())
            //    .Select(x => new ReadAdvertisementDto
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //        Description = x.Description,
            //        AdvertisementType = x.AdvertisementType,
            //        ReadAdvertisementDetailsDto = new ReadAdvertisementDetailsDto
            //        {
            //            Id = x.AdvertisementDetails.Id,
            //            DealType = x.AdvertisementDetails.DealType,
            //            Location = x.AdvertisementDetails.Location,
            //            Price = x.AdvertisementDetails.Price,
            //            SquareMeter = x.AdvertisementDetails.SquareMeter,
            //        }
            //    });
        }

        public async Task<ReadAdvertisementDto> GetByIdAsync(int id)
        {
            var existingAd = await _advertisementRepository.GetByIdAsync(id);
            

            if (existingAd == null)
                throw new InvalidOperationException(nameof(existingAd));

            return new ReadAdvertisementDto
            {
                Id = existingAd.Id,
                Name = existingAd.Name,
                Description = existingAd.Description,
                AdvertisementType = existingAd.AdvertisementType,
                ReadAdvertisementDetailsDto = new ReadAdvertisementDetailsDto
                {
                    Id = existingAd.AdvertisementDetails.Id,
                    DealType = existingAd.AdvertisementDetails.DealType,
                    Location = existingAd.AdvertisementDetails.Location,
                    Price = existingAd.AdvertisementDetails.Price,
                    SquareMeter = existingAd.AdvertisementDetails.SquareMeter,
                }
            };
        }
    }
}
