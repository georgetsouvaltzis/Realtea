using MediatR;
using Realtea.Core.Commands.Payment;
using Realtea.Core.Enums;
using Realtea.Core.Exceptions;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.ValueObjects;

namespace Realtea.Core.Handlers.Commands.Payment
{
    public class AddBalanceCommandHandler : IRequestHandler<AddBalanceCommand>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserRepository _userRepository;
        public AddBalanceCommandHandler(IPaymentRepository paymentRepository, IUserRepository userRepository)
        {
            _paymentRepository = paymentRepository;
            _userRepository = userRepository;
        }
        public async Task Handle(AddBalanceCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByIdAsync(request.UserId.ToString());

            if (existingUser == null)
                throw new ApiException($"User with ID: {request.UserId} does not exist.", FailureType.Absent);

            // TODO: DO NOT FORGET ABOUT PAYMENT ID
            await _paymentRepository.CreateAsync(Entities
                .Payment
                .Create(existingUser.Id,
                PaymentDetail.Card,
                DateTimeOffset.UtcNow,
                1,
                Money.Create(1.0m)));
        }
    }
}

