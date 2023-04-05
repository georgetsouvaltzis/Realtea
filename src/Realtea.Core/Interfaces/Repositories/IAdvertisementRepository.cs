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

        IQueryable<Advertisement?> GetByCondition(Expression<Func<Advertisement, bool>> expr = default);

        Task<Advertisement> UpdateAsync(Advertisement advertisement);
    }
}
