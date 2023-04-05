

//using Realtea.Core.DTOs.Advertisement;
//using Realtea.Core.Entities;
//using Realtea.Core.Enums;
//using Realtea.Core.Interfaces;
//using Realtea.Core.Interfaces.Repositories;
//using Realtea.Core.Interfaces.Services;
//using Realtea.Core.Models;

//namespace Realtea.Core.Services
//{
//    public class AdvertisementService : IAdvertisementService
//    {
//        private readonly IAdvertisementRepository _advertisementRepository;
//        private readonly PaymentService _paymentService;
//        private readonly IUserService _userService;
//        //private readonly IPaymentRepository _paymentRepository;

//        //private readonly UserManager<User> _userManager;

//        //public AdvertisementService(IAdvertisementRepository advertisementRepository, UserManager<User> userManager,
//        //    IPaymentRepository paymentRepository)
//        //{
//        //    _advertisementRepository = advertisementRepository;
//        //    _userManager = userManager;
//        //    _paymentReposiÍtory = paymentRepository;
//        //}

//        public AdvertisementService(IAdvertisementRepository advertisementRepository, IUserService userService, PaymentService paymentService)
//        {
//            _advertisementRepository = advertisementRepository;
//            _userService = userService;
//            _paymentService = paymentService;
//        }

//        public async Task<int> AddAsync(CreateAdvertisementDto createAdvertisementDto, int userId)
//        {
//            _ = createAdvertisementDto ?? throw new ArgumentNullException(nameof(createAdvertisementDto));
//            _ = createAdvertisementDto.CreateAdvertisementDetailsDto ?? throw new ArgumentNullException(nameof(createAdvertisementDto.CreateAdvertisementDetailsDto));

 
//            var existingUser = await _userManager.FindByIdAsync(userId.ToString());

//            if (existingUser == null)
//                throw new InvalidOperationException(nameof(existingUser));

//            if (string.IsNullOrEmpty(createAdvertisementDto.Name))
//            {
//                throw new InvalidOperationException(nameof(createAdvertisementDto.Name));
//            }

//            if (string.IsNullOrEmpty(createAdvertisementDto.Description))
//            {
//                throw new InvalidOperationException(nameof(createAdvertisementDto.Description));
//            }

//            var another = _advertisementRepository.GetAllAsync().GetAwaiter().GetResult().Where(x => x.UserId == existingUser.Id).Count();
//            // Need to change this logic so it returns IEnumerable
//            //var existingAdCount = _advertisementRepository.GetByCondition(x => x.Id == existingUser.Id).Count();

//            // Can move to domain later.
//            if (another >= 5 && existingUser.UserType == UserType.Regular && createAdvertisementDto.AdvertisementType == AdvertisementType.Free)
//                throw new InvalidOperationException("Unable to add advertisement. You have reached your limit. Please upgrade your account type to Broker. Or consider using Paid ads.");

//            if (createAdvertisementDto.AdvertisementType == AdvertisementType.Paid)
//            {
//                if (!existingUser.UserBalance.IsCapableOfPayment)
//                {
//                    throw new InvalidOperationException("Insufficient balance.");
//                }

//                existingUser.UserBalance.Balance -= 0.20m;
//                _userService.CreateAsync();
//                //await _paymentRepository.CreateAsync(new Payment
//                //{
//                //    PaidAmount = 0.20m,
//                //    //AdvertisementId = 100,// Should fire an Event in order to notify about payment and update user/ad?.
//                //    PaymentDetail = PaymentDetail.Balance,
//                //    PaymentMadeAt = DateTimeOffset.UtcNow,
//                //    UserId = userId,
//                //});
//            }

//            var newAdvertisement = new Advertisement
//            {
//                Name = createAdvertisementDto.Name,
//                UserId = existingUser.Id,
//                Description = createAdvertisementDto.Description,
//                AdvertisementDetails = new AdvertisementDetails
//                {
//                    DealType = createAdvertisementDto.CreateAdvertisementDetailsDto.DealType,
//                    Location = createAdvertisementDto.CreateAdvertisementDetailsDto.Location,
//                    Price = createAdvertisementDto.CreateAdvertisementDetailsDto.Price,
//                    SquareMeter = createAdvertisementDto.CreateAdvertisementDetailsDto.SquareMeter,
//                }
//            };

//            await _advertisementRepository.AddAsync(newAdvertisement);

//            return newAdvertisement.Id;
//        }

//        public async Task<IEnumerable<ReadAdvertisementDto>> GetAllAsync(AdvertisementDescription advertisementDescription)
//        {

//            var f = await _advertisementRepository.GetAsQ();

//            if (advertisementDescription.DealType.HasValue)
//                f = f.Where(x => x.AdvertisementDetails.DealType == advertisementDescription.DealType.Value);

//            if (advertisementDescription.PriceFrom.HasValue)
//                f = f.Where(x => x.AdvertisementDetails.Price >= advertisementDescription.PriceFrom);

//            if (advertisementDescription.PriceTo.HasValue)
//                f = f.Where(x => x.AdvertisementDetails.Price <= advertisementDescription.PriceTo);

//            if (advertisementDescription.SqFrom.HasValue)
//                f = f.Where(x => x.AdvertisementDetails.SquareMeter >= advertisementDescription.SqFrom);

//            if (advertisementDescription.SqTo.HasValue)
//                f = f.Where(x => x.AdvertisementDetails.SquareMeter <= advertisementDescription.SqTo);

//            if (advertisementDescription.Location.HasValue)
//                f = f.Where(x => x.AdvertisementDetails.Location == advertisementDescription.Location.Value);

//            return f.Select(x => new ReadAdvertisementDto
//            {
//                Id = x.Id,
//                Name = x.Name,
//                Description = x.Description,
//                AdvertisementType = x.AdvertisementType,
//                ReadAdvertisementDetailsDto = new ReadAdvertisementDetailsDto
//                {
//                    Id = x.AdvertisementDetails.Id,
//                    DealType = x.AdvertisementDetails.DealType,
//                    Location = x.AdvertisementDetails.Location,
//                    Price = x.AdvertisementDetails.Price,
//                    SquareMeter = x.AdvertisementDetails.SquareMeter,
//                }
//            });
//        }

//        public async Task<IEnumerable<Advertisement>> GetActiveAdsOrderedByPaidAds()
//        {
//            var now = DateTimeOffset.UtcNow;

//            var ads = _advertisementRepository
//                .GetByCondition(x => x.IsActive && x.IsActiveUntil != null && x.IsActiveUntil <= now)
//                .OrderByDescending(x => x.AdvertisementType)
//                .ToList();


//            // Should map to DTO and send it back.
//            return Enumerable.Empty<Advertisement>();
//        }

//        public async Task<ReadAdvertisementDto> GetByIdAsync(int id)
//        {
//            throw new NotImplementedException();
//            //var existingAd = await _advertisementRepository.GetByIdAsync(id);

//            //if (existingAd == null)
//            //    throw new InvalidOperationException(nameof(existingAd));

//            //return new ReadAdvertisementDto
//            //{
//            //    Id = existingAd.Id,
//            //    UserId = existingAd.UserId,
//            //    Name = existingAd.Name,
//            //    Description = existingAd.Description,
//            //    AdvertisementType = existingAd.AdvertisementType,
//            //    IsActive = existingAd.IsActive,
//            //    ReadAdvertisementDetailsDto = new ReadAdvertisementDetailsDto
//            //    {
//            //        Id = existingAd.AdvertisementDetails.Id,
//            //        DealType = existingAd.AdvertisementDetails.DealType,
//            //        Location = existingAd.AdvertisementDetails.Location,
//            //        Price = existingAd.AdvertisementDetails.Price,
//            //        SquareMeter = existingAd.AdvertisementDetails.SquareMeter,
//            //    }
//            //};
//        }

//        public async Task InvalidateAsync(int id)
//        {
//            var existingAd = await _advertisementRepository.GetByIdAsync(id);

//            if (existingAd == null)
//                throw new InvalidOperationException(nameof(existingAd));

//            await _advertisementRepository.InvalidateAsync(id);
//        }

//        public async Task<UpdateAdvertisementDto> UpdateAsync(int id, int userId, UpdateAdvertisementDto updateAdvertisementDto)
//        {
//            throw new NotImplementedException();
//            //var existingAd = await _advertisementRepository.GetByIdAsync(id);

//            //if (existingAd == null)
//            //    throw new InvalidOperationException(nameof(existingAd));


//            //existingAd.Name = updateAdvertisementDto.Name ?? existingAd.Name;
//            //existingAd.Description = updateAdvertisementDto.Description ?? existingAd.Description;
//            //existingAd.IsActive = updateAdvertisementDto.IsActive ?? existingAd.IsActive;

//            //if (updateAdvertisementDto.UpdateAdvertisementDetailsDto != null)
//            //{
//            //    existingAd.AdvertisementDetails.DealType = updateAdvertisementDto.UpdateAdvertisementDetailsDto.DealType ?? existingAd.AdvertisementDetails.DealType;
//            //    existingAd.AdvertisementDetails.Location = updateAdvertisementDto.UpdateAdvertisementDetailsDto.Location ?? existingAd.AdvertisementDetails.Location;
//            //    existingAd.AdvertisementDetails.Price = updateAdvertisementDto.UpdateAdvertisementDetailsDto.Price ?? existingAd.AdvertisementDetails.Price;
//            //    existingAd.AdvertisementDetails.SquareMeter = updateAdvertisementDto.UpdateAdvertisementDetailsDto.Price ?? existingAd.AdvertisementDetails.Price;
//            //}


//            //var modifiedAd = await _advertisementRepository.UpdateAsync(existingAd);

//            //return new UpdateAdvertisementDto
//            //{
//            //    Name = modifiedAd.Name,
//            //    Description = modifiedAd.Description,
//            //    IsActive = modifiedAd.IsActive,
//            //    AdvertisementType = modifiedAd.AdvertisementType,
//            //    UpdateAdvertisementDetailsDto = new UpdateAdvertisementDetailsDto
//            //    {
//            //        DealType = modifiedAd.AdvertisementDetails.DealType,
//            //        Location = modifiedAd.AdvertisementDetails.Location,
//            //        SquareMeter = modifiedAd.AdvertisementDetails.SquareMeter,
//            //        Price = modifiedAd.AdvertisementDetails.Price,
//            //    }
//            //};
//        }
//    }
//}
