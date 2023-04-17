﻿using System;
using MediatR;
using Realtea.Core.Commands.Payment;
using Realtea.Core.Entities;
using Realtea.Core.Enums;
using Realtea.Core.Interfaces.Repositories;

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
                throw new InvalidOperationException(nameof(existingUser));

            await _paymentRepository.CreateAsync(new Entities.Payment
            {
                PaymentMadeAt = DateTimeOffset.UtcNow,
                PaidAmount = 1.0m,
                PaymentDetail = PaymentDetail.Card,
                UserId = existingUser.Id,
            });
        }
    }
}

