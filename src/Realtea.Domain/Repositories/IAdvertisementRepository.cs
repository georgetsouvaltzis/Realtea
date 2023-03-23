using Realtea.Domain.Entities;
using System.Linq.Expressions;

namespace Realtea.Domain.Repositories
{
    public interface IAdvertisementRepository
    {
        Task<int> AddAsync(Advertisement advertisement);

        Task<IEnumerable<Advertisement>> GetAllAsync();

        Task<Advertisement?> GetByIdAsync(int id);
    }
}
