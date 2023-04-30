using Realtea.Core.Entities;

namespace Realtea.Core.Interfaces.Repositories
{
    /// <summary>
    /// Repository for Advertisement related actions.
    /// </summary>
    public interface IAdvertisementRepository
    {
        /// <summary>
        /// Create new advertisement.
        /// </summary>
        /// <param name="advertisement">Advertisement-related data.</param>
        /// <returns>ID of the created advertisement.</returns>
        Task<int> AddAsync(Advertisement advertisement);

        /// <summary>
        /// Retrieves all the advertisements from the Db.
        /// </summary>
        Task<IEnumerable<Advertisement>> GetAllAsync();

        /// <summary>
        /// Retrieves Advertisement by given ID.
        /// </summary>
        /// <param name="id">ID of the advertisement we want to retrieve.</param>
        /// <returns>Advertisement if it was found, othwerise null.</returns>
        Task<Advertisement?> GetByIdAsync(int id);

        /// <summary>
        /// Invalidates Advertisement which sets IsActive property to false.
        /// </summary>
        /// <param name="id">ID of the advertisement we want to invalidate.</param>
        /// <returns></returns>
        Task InvalidateAsync(int id);

        /// <summary>
        /// Updates advertisement.
        /// </summary>
        /// <param name="advertisement">Contains all the data that is required for update.</param>
        /// <returns>Updated advertisement.</returns>
        Task<Advertisement> UpdateAsync(Advertisement advertisement);

        /// <summary>
        /// Returns Advertisement from the database as Queryable.
        /// </summary>
        IQueryable<Advertisement?> GetAsQueryable();

        /// <summary>
        /// Determines whether given user has exceeded advertisements or not.
        /// </summary>
        /// <param name="userId">ID of the user.</param>
        /// <returns>true if has exceeded, otherwise false.</returns>
        bool HasExceededFreeAds(int userId);
    }
}
