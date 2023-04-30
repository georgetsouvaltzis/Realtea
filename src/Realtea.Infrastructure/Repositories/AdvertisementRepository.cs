using Microsoft.EntityFrameworkCore;
using Realtea.Core.Entities;
using Realtea.Core.Enums;
using Realtea.Core.Interfaces.Repositories;

namespace Realtea.Infrastructure.Repositories
{
    /// <inheritdoc/>
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly RealTeaDbContext _db;

        public AdvertisementRepository(RealTeaDbContext dbContext)
        {
            _db = dbContext;
        }

        /// <inheritdoc/>
        public async Task<int> AddAsync(Advertisement advertisement)
        {
            await _db.AddAsync(advertisement);
            await _db.SaveChangesAsync();

            return advertisement.Id;
        }

        /// <inheritdoc/>
        public async Task InvalidateAsync(int id)
        {
            var existingAd = await GetByIdAsync(id);

            existingAd.SetIsActive(false);

            _db.Entry(existingAd).State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Advertisement>> GetAllAsync()
        {
            return await _db
                .Advertisements
                .Include(x => x.User)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <inheritdoc/>
        public IQueryable<Advertisement?> GetAsQueryable()
        {
            return _db
                .Advertisements
                .Include(x => x.User)
                .Include(x => x.Price)
                .Include(x => x.SquareMeter)
                .AsQueryable();

        }

        /// <inheritdoc/>
        public async Task<Advertisement?> GetByIdAsync(int id)
        {
            return await _db
                .Advertisements
                .Include(x => x.User)
                .Include(x => x.Price)
                .Include(x => x.SquareMeter)
                .FirstOrDefaultAsync(x => x.Id == id);


        }

        /// <inheritdoc/>
        public async Task<Advertisement> UpdateAsync(Advertisement advertisement)
        {
            _db.Advertisements.Update(advertisement);

            await _db.SaveChangesAsync();

            return advertisement;
        }

        /// <inheritdoc/>
        public bool HasExceededFreeAds(int userId)
        {
            const int FreeAdsLimit = 5;

            var freeAdsCount = _db
                .Advertisements
                .Where(x => x.Id == userId && x.AdvertisementType == AdvertisementType.Free)
                .Count();

            return freeAdsCount >= FreeAdsLimit;
        }
    }
}

