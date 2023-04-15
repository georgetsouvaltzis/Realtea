using System;
using MediatR;
using Realtea.Core.Commands.Advertisement;
using Realtea.Core.Entities;
using Realtea.Core.Enums;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.Responses.Advertisement;

namespace Realtea.Core.Handlers.Commands.Advertisement
{
    public class CreateAdvertisementCommandHandler : IRequestHandler<CreateAdvertisementCommand, CreateAdvertisementResponse>
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IUserRepository _userRepository;

        public CreateAdvertisementCommandHandler(IAdvertisementRepository advertisementRepository,
            IUserRepository userRepository)
        {
            _advertisementRepository = advertisementRepository;
            _userRepository = userRepository;
        }

        public async Task<CreateAdvertisementResponse> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));
            _ = request.UpdateAdvertisementDetails ?? throw new ArgumentNullException(nameof(request.UpdateAdvertisementDetails));


            var existingUser = await _userRepository.GetByIdAsync(request.UserId);
            //var existingUser = await _userManager.FindByIdAsync(userId.ToString());

            if (existingUser == null)
                throw new InvalidOperationException(nameof(existingUser));

            if (string.IsNullOrEmpty(request.Name))
            {
                throw new InvalidOperationException(nameof(request.Name));
            }

            if (string.IsNullOrEmpty(request.Description))
            {
                throw new InvalidOperationException(nameof(request.Description));
            }

            var another = _advertisementRepository.GetAllAsync().GetAwaiter().GetResult().Where(x => x.UserId == existingUser.Id).Count();
            // Need to change this logic so it returns IEnumerable
            var existingAdCount = _advertisementRepository.GetByCondition(x => x.Id == existingUser.Id).Count();

            // Can move to domain later.
            if (another >= 5 && existingUser.UserType == UserType.Regular && request.AdvertisementType.Value == AdvertisementType.Free)
                throw new InvalidOperationException("Unable to add advertisement. You have reached your limit. Please upgrade your account type to Broker. Or consider using Paid ads.");

            if (request.AdvertisementType == AdvertisementType.Paid && !existingUser.UserBalance.IsCapableOfPayment)
            {
                throw new InvalidOperationException("Insufficient balance.");
            }
            existingUser.UserBalance.Balance -= 0.20m;
            //await _paymentRepository.CreateAsync(new Payment
            //{
            //    PaidAmount = 0.20m,
            //    //AdvertisementId = 100,// Should fire an Event in order to notify about payment and update user/ad?.
            //    PaymentDetail = PaymentDetail.Balance,
            //    PaymentMadeAt = DateTimeOffset.UtcNow,
            //    UserId = userId,
            //});

            var newAdvertisement = new Realtea.Core.Entities.Advertisement
            {
                Name = request.Name,
                UserId = existingUser.Id,
                Description = request.Description,
                AdvertisementDetails = new AdvertisementDetails
                {
                    // TODO: DO NOT FORGET ABOUT THIS
                    //DealType = request.UpdateAdvertisementDetails.DealType,
                    //Location = request.UpdateAdvertisementDetails.Location.Value,
                    Price = request.UpdateAdvertisementDetails.Price.Value,
                    SquareMeter = request.UpdateAdvertisementDetails.SquareMeter.Value,
                }
            };

            await _advertisementRepository.AddAsync(newAdvertisement);

            return new CreateAdvertisementResponse
            {
                Id = newAdvertisement.Id
            };
        }
    }
}


