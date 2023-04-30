using Realtea.Core.Entities;

namespace Realtea.Core.Interfaces.Repositories
{
    /// <summary>
    /// Repository for User.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves User by id.
        /// </summary>
        /// <param name="userId">ID of the user we want to retrieve.</param>
        /// <returns>Found User.</returns>
        Task<User> GetByIdAsync(string userId);

        /// <summary>
        /// Retrieves user by Username.
        /// </summary>
        /// <param name="username">username of the user we want to retrieve.</param>
        /// <returns></returns>
        Task<User> GetByUsernameAsync(string username);

        /// <summary>
        /// Creates new User.
        /// </summary>
        /// <param name="user">User related information.</param>
        /// <param name="password">Password for the user.</param>
        /// <returns>ID of the created user.</returns>
        Task<int> CreateAsync(User user, string password);

        /// <summary>
        /// Updates user data.
        /// </summary>
        /// <param name="user">Updated data of the user.</param>
        Task UpdateAsync(User user);

        /// <summary>
        /// Deletes user.
        /// </summary>
        /// <param name="userId">ID of the user we want to delete.</param>
        Task DeleteAsync(int userId);

        /// <summary>
        /// Determines whether user is in Broker role or not.
        /// </summary>
        /// <param name="userId">ID of the user we want to check.</param>
        /// <returns>True if user is In broker role, otherwise false.</returns>
        Task<bool> IsInBrokerRoleAsync(int userId);

        /// <summary>
        /// Upgrades user's role to be part of Broker.
        /// </summary>
        /// <param name="userId">ID oft the user we want to upgrade.</param>
        Task UpgradeToBrokerAsync(int userId);
    }
}

