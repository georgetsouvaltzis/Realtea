using System;
using Realtea.Core.DTOs.Advertisement;
using Realtea.Core.Entities;

namespace Realtea.Core.Interfaces.Services
{
    public interface IUserService
    {
        //Task<IEnumerable<ReadAdvertisementDto>> GetAds(int userId);

        Task<User> GetByIdAsync(string userId);
        Task UpgradeAccountAsync(string userId);

        Task UpdateAsync(int userId);
    }
}

