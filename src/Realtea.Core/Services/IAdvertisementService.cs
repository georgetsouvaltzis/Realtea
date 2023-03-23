using Realtea.Core.DTOs.Advertisement;
using Realtea.Domain.Entities;
using Realtea.Domain.Repositories;

namespace Realtea.Core.Services
{
    public interface IAdvertisementService
    {
        Task AddAsync(CreateAdvertisementDto createAdvertisementDto);
    }

    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public AdvertisementService(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public async Task AddAsync(CreateAdvertisementDto createAdvertisementDto)
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
        }
    }
}
