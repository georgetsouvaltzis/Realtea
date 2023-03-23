using Realtea.Domain.Entities;
using Realtea.Domain.Repositories;
using Realtea.Infrastructure;

namespace Realtea.Core.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly RealTeaDbContext _db;
        public AdvertisementRepository(RealTeaDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task AddAsync(Advertisement advertisement)
        {
            await _db.AddAsync(advertisement);
            await _db.SaveChangesAsync();
        }
    }
}
