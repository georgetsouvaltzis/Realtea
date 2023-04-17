

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Realtea.Core.Entities;
using Realtea.Core.Interfaces.Repositories;

namespace Realtea.Infrastructure.Repositories
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

        public async Task InvalidateAsync(int id)
        {
            //var existingAd = await GetByIdAsync(id);

            var existingAd = new Advertisement
            {
                Id = id,
                IsActive = false,
            };
            //existingAd!.IsActive = false;

            _db.Entry<Advertisement>(existingAd).State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Advertisement>> GetAllAsync()
        {
            return await _db
                .Advertisements
                .Include(x => x.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public IQueryable<Advertisement?> GetAsQueryable()
        {
            return _db
                .Advertisements
                .Include(x => x.User)
                .AsQueryable();
            
        }
        public async Task<Advertisement?> GetByIdAsync(int id)
        {
            return await _db
                .Advertisements
                .Include(x => x.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);


        }

        public async Task<Advertisement> UpdateAsync(Advertisement advertisement)
        {
            _db.Advertisements.Update(advertisement);

            await _db.SaveChangesAsync();

            return advertisement;
        }
    }
}

