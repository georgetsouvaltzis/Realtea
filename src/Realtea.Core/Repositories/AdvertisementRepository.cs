using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Realtea.Domain.Entities;
using Realtea.Domain.Repositories;
using Realtea.Infrastructure;

namespace Realtea.Core.Repositories
{
    //public class AdvertisementRepository : IAdvertisementRepository
    //{
    //    private readonly RealTeaDbContext _db;

    //    public AdvertisementRepository(RealTeaDbContext dbContext)
    //    {
    //        _db = dbContext;
    //    }

    //    public async Task<int> AddAsync(Advertisement advertisement)
    //    {
    //        await _db.AddAsync(advertisement);
    //        await _db.SaveChangesAsync();

    //        return advertisement.Id;
    //    }

    //    public async Task InvalidateAsync(int id)
    //    {
    //        var existingAd = await GetByIdAsync(id);

    //        existingAd!.IsActive = false;

    //        _db.Entry<Advertisement>(existingAd).State = EntityState.Modified;

    //        await _db.SaveChangesAsync();
    //    }

    //    public async Task<IEnumerable<Advertisement>> GetAllAsync()
    //    {
    //        return await _db
    //            .Advertisements
    //            .Include(x => x.AdvertisementDetails)
    //            .Include(x => x.User)
    //            .AsNoTracking()
    //            .ToListAsync();
    //    }

    //    public async Task<Advertisement?> GetByIdAsync(int id)
    //    {
    //        return await _db
    //            .Advertisements
    //            .Include(x => x.AdvertisementDetails)
    //            .Include(x => x.User)
    //            .AsNoTracking()
    //            .FirstOrDefaultAsync(x => x.Id == id);


    //    }
    //    public IQueryable<Advertisement?> GetByCondition(Expression<Func<Advertisement, bool>> expr = default)
    //    {
    //        return _db
    //            .Advertisements
    //            .Include(x => x.AdvertisementDetails)
    //            .Include(x => x.User)
    //            .Where(expr);
    //    }

    //    public async Task<Advertisement> UpdateAsync(Advertisement advertisement)
    //    {
    //        _db.Advertisements.Update(advertisement);

    //        await _db.SaveChangesAsync();

    //        return advertisement;
    //    }
    //}
}
