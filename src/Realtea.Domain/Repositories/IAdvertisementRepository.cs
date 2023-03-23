using Realtea.Domain.Entities;

namespace Realtea.Domain.Repositories
{
    public interface IAdvertisementRepository
    {
        Task AddAsync(Advertisement advertisement);
    }
}
