using System;
using MediatR;
using Realtea.Core.Commands.Advertisement;
using Realtea.Core.Entities;
using Realtea.Core.Enums;
using Realtea.Core.Exceptions;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.Results.Advertisement;

namespace Realtea.Core.Handlers.Commands.Advertisement
{
    public class CreateAdvertisementCommandHandler : IRequestHandler<CreateAdvertisementCommand, CreateAdvertisementResult>
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserRepository _userRepository;

        public CreateAdvertisementCommandHandler(IAdvertisementRepository advertisementRepository,
            IUserRepository userRepository,
            IPaymentRepository paymentRepository)
        {
            _advertisementRepository = advertisementRepository;
            _userRepository = userRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<CreateAdvertisementResult> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            var existingUser = await _userRepository.GetByIdAsync(request.UserId.ToString());

            // It will be moved to ValidationFilter.
            if (string.IsNullOrEmpty(request.Name))
            {
                throw new InvalidOperationException(nameof(request.Name));
            }

            if (string.IsNullOrEmpty(request.Description))
            {
                throw new InvalidOperationException(nameof(request.Description));
            }

            // Can move to domain later.
            if (_advertisementRepository.HasExceededFreeAds(existingUser.Id) && existingUser.UserType == UserType.Regular && request.AdvertisementType == AdvertisementType.Free)
                throw new ApiException("Unable to add advertisement. You have reached your limit. Please upgrade your account type to Broker. Or consider using Paid ads.", FailureType.Insufficient);

            if (request.AdvertisementType == AdvertisementType.Paid && !existingUser.UserBalance.IsCapableOfPayment)
            {
                throw new ApiException("Insufficient balance.", FailureType.Insufficient);
            }

            var newAdvertisement = new Realtea.Core.Entities.Advertisement
            {
                Name = request.Name,
                UserId = existingUser.Id,
                Description = request.Description,
                DealType = request.DealType,
                Location = request.Location,
                SquareMeter = request.SquareMeter,
            };

            await _advertisementRepository.AddAsync(newAdvertisement);

            // Probably could be some kind of event to fire it and then update value of it.
            await _paymentRepository.CreateAsync(new Entities.Payment
            {
                AdvertisementId = newAdvertisement.Id,
                PaidAmount = 0.2m,
                PaymentMadeAt = DateTimeOffset.Now,
                PaymentDetail = PaymentDetail.Balance,
                UserId = existingUser.Id,
            });

            return new CreateAdvertisementResult
            {
                Id = newAdvertisement.Id
            };
        }
    }
}


