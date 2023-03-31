using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Realtea.Core.DTOs.Advertisement;
using Realtea.Domain.Entities;
using Realtea.Domain.Enums;
using Realtea.Domain.Repositories;
using Realtea.Infrastructure;

namespace Realtea.Core.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ReadAdvertisementDto>> GetAds(int userId);

        Task UpdateAccountAsync(string userId);
    }


    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly RealTeaDbContext _dbContext;

        public UserService(UserManager<User> userManager, RealTeaDbContext context)
        {
            _userManager = userManager;
            _dbContext = context;

        }

        public async Task UpdateAccountAsync(string userId)
        {
            var existingUser = await _userManager.FindByIdAsync(userId);

            if (existingUser == null)
                throw new InvalidOperationException(nameof(existingUser));


            existingUser.UserType = UserType.Broker;

            await _userManager.UpdateAsync(existingUser);
            await _dbContext.SaveChangesAsync();

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

