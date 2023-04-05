using System;
using Realtea.Core.Entities;

namespace Realtea.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int userId);
    }
}

