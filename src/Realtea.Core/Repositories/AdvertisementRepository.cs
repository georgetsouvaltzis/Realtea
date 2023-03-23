using Microsoft.EntityFrameworkCore;
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

        public async Task<int> AddAsync(Advertisement advertisement)
        {
            await _db.AddAsync(advertisement);
            await _db.SaveChangesAsync();

            return advertisement.Id;
        }

        public async Task<IEnumerable<Advertisement>> GetAllAsync()
        {

            return await _db
                .Advertisements
                .Include(x => x.AdvertisementDetails)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Advertisement?> GetByIdAsync(int id)
        {
            return await _db
                .Advertisements
                .Include(x => x.AdvertisementDetails)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
