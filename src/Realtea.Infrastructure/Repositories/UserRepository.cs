using System;
using Microsoft.AspNetCore.Identity;
using Realtea.Core.Entities;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Infrastructure.Identity;

namespace Realtea.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RealTeaDbContext _dbContext;
		public UserRepository(UserManager<ApplicationUser> userManager, RealTeaDbContext dbContext)
		{
            _userManager = userManager;
            _dbContext = dbContext;
		}

        public async Task<int> CreateAsync(User user, string password)
        {
            var newApplicationUser = new ApplicationUser
            { 
                UserName = user.UserName,
            };

            await _userManager.CreateAsync(newApplicationUser, password);

            user.Id = newApplicationUser.Id;

            await _dbContext.AddAsync(user);
            await _dbContext.AddAsync(new UserBalance
            {
                Balance = 1.00m,
                UserId = user.Id,
            });
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task<User> GetByIdAsync(string userId)
        {
            var existingUser = await _userManager.FindByIdAsync(userId);

            if (existingUser == null) return null;

            var existingBalance = _dbContext.Set<UserBalance>().Single(x => x.UserId == existingUser.Id);

            return new User
            {
                Id = existingUser.Id,
                //FirstName = existingUser.FirstName,
                //LastName = existingUser.LastName,
                UserName = existingUser.UserName,
                UserBalance = existingBalance,
            };
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var existingUser = await _userManager.FindByNameAsync(username);

            if (existingUser == null) return null;

            return new User
            {
                Id = existingUser.Id,
                //FirstName = existingUser.FirstName,
                //LastName = existingUser.LastName,
                UserName = existingUser.UserName,
            };
        }
    }
}

