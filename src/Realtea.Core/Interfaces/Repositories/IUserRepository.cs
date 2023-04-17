using System;
using Realtea.Core.Entities;

namespace Realtea.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string userId);

        Task<User> GetByUsernameAsync(string username);

        Task<int> CreateAsync(User user, string password);

        Task UpdateAsync(User user);

        Task DeleteAsync(int userId);
    }
}

