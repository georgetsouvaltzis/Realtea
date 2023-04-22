using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Realtea.Core.Entities;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Infrastructure.Identity;

namespace Realtea.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string BrokerRole = "Broker";
        private const string NormalRole = "Normal";

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RealTeaDbContext _dbContext;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public UserRepository(UserManager<ApplicationUser> userManager, RealTeaDbContext dbContext, RoleManager<ApplicationRole> roleManager)
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

            var result = await _userManager.CreateAsync(newApplicationUser, password);

            user.Id = newApplicationUser.Id;

            await _dbContext.AddAsync(user);
            await _dbContext.AddAsync(new UserBalance
            {
                Balance = 1.00m,
                UserId = user.Id,
            });

            await _userManager.AddToRoleAsync(newApplicationUser, "Normal");
            await _dbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task DeleteAsync(int userId)
        {
            var existingIdentityUser = await _userManager.FindByIdAsync(userId.ToString());

            await _userManager.DeleteAsync(existingIdentityUser);
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

        public async Task<bool> IsInBrokerRoleAsync(int userId)
        {
            var existingUser = await _userManager.FindByIdAsync(userId.ToString());

            var roles = await _userManager.GetRolesAsync(existingUser);

            return roles.First().Equals(BrokerRole);
        }

        public async Task UpdateAsync(User user)
        {
            var identityUser = await _userManager.FindByIdAsync(user.Id.ToString());

            if (user.FirstName != null)
                identityUser.FirstName = user.FirstName;

            if (user.LastName != null)
                identityUser.LastName = user.LastName;

            if (user.Email != null)
                identityUser.Email = user.Email;

            await _userManager.UpdateAsync(identityUser);

            _dbContext.Entry<User>(user).State = EntityState.Modified;
            _dbContext.Set<User>().Update(user);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpgradeToBrokerAsync(int userId)
        {
            var existingUser = await _userManager.FindByIdAsync(userId.ToString());
            var existingRoles = await _userManager.GetRolesAsync(existingUser);

            if (existingRoles.Any(x => x.Equals(NormalRole)))
                await _userManager.RemoveFromRoleAsync(existingUser, NormalRole);
            
            await _userManager.AddToRoleAsync(existingUser, BrokerRole);
        }
    }
}

