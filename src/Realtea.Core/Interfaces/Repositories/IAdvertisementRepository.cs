using System;
using System.Linq.Expressions;
using Realtea.Core.Entities;

namespace Realtea.Core.Interfaces.Repositories
{
    public interface IAdvertisementRepository
    {
        Task<int> AddAsync(Advertisement advertisement);

        Task<IEnumerable<Advertisement>> GetAllAsync();

        Task<Advertisement?> GetByIdAsync(int id);

        Task InvalidateAsync(int id);

        Task<Advertisement> UpdateAsync(Advertisement advertisement);

        IQueryable<Advertisement?> GetAsQueryable();
    }
}
