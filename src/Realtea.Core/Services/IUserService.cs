using System;
using Microsoft.EntityFrameworkCore;
using Realtea.Core.DTOs.Advertisement;
using Realtea.Domain.Repositories;

namespace Realtea.Core.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ReadAdvertisementDto>> GetAds(int userId);
    }


    public class UserService : IUserService
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public UserService(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;

        }


        public async Task<IEnumerable<ReadAdvertisementDto>> GetAds(int userId)
        {
            var ads = await _advertisementRepository.GetByCondition(x => x.UserId == userId).ToListAsync();

            return ads.Select(x => new ReadAdvertisementDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                AdvertisementType = x.AdvertisementType,
                ReadAdvertisementDetailsDto = new ReadAdvertisementDetailsDto
                {
                    Id = x.AdvertisementDetailsId,
                    DealType = x.AdvertisementDetails.DealType,
                    Location = x.AdvertisementDetails.Location,
                    Price = x.AdvertisementDetails.Price,
                    SquareMeter = x.AdvertisementDetails.SquareMeter,
                }
            });
        }
    }
}

