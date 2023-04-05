
//using Realtea.Core.Entities;
//using Realtea.Core.Enums;
//using Realtea.Core.Interfaces.Repositories;
//using Realtea.Core.Interfaces.Services;

//namespace Realtea.Core.Services
//{

//    public class
//        UserService : IUserService
//    {
//        //private readonly UserManager<User> _userManager;
//        //private readonly IAdvertisementRepository _advertisementRepository;
//        private readonly IUserRepository _userRepository;
//        //private readonly RealTeaDbContext _dbContext;

//        //public UserService(UserManager<User> userManager, RealTeaDbContext context)
//        //{
//        //    _userManager = userManager;
//        //    //_dbContext = context;
//        //}

//        public async Task UpgradeAccountAsync(string userId)
//        {
//            var existingUser = await _userManager.FindByIdAsync(userId);

//            if (existingUser == null)
//                throw new InvalidOperationException(nameof(existingUser));


//            existingUser.UserType = UserType.Broker;

//            await _userManager.UpdateAsync(existingUser);
//            //await _dbContext.SaveChangesAsync();

//        }

//        //public async Task<IEnumerable<ReadAdvertisementDto>> GetAds(int userId)
//        //{
//        //    //var ads = await _advertisementRepository.GetByCondition(x => x.UserId == userId).ToListAsync();
//        //    var ads = _advertisementRepository.GetByCondition(x => x.UserId == userId).ToList();

//        //    return ads.Select(x => new ReadAdvertisementDto
//        //    {
//        //        Id = x.Id,
//        //        Name = x.Name,
//        //        Description = x.Description,
//        //        AdvertisementType = x.AdvertisementType,
//        //        ReadAdvertisementDetailsDto = new ReadAdvertisementDetailsDto
//        //        {
//        //            Id = x.AdvertisementDetailsId,
//        //            DealType = x.AdvertisementDetails.DealType,
//        //            Location = x.AdvertisementDetails.Location,
//        //            Price = x.AdvertisementDetails.Price,
//        //            SquareMeter = x.AdvertisementDetails.SquareMeter,
//        //        }
//        //    });
//        //}

//        public Task UpdateAsync(int userId)
//        {
//            throw new InvalidCastException();
//        }

//        public Task<User> GetByIdAsync(string userId)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}

