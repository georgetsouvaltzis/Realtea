using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Realtea.App.HttpContextWrapper;
using Realtea.Core.Commands.Payment;
using Realtea.Core.Entities;
using Realtea.Core.Enums;
using Realtea.Core.Interfaces;
using Realtea.Core.Interfaces.Repositories;

namespace Realtea.App.Controllers.V1
{

	[Authorize]
	public class PaymentsController : V1ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly IPaymentRepository _paymentRepository;

        public PaymentsController(IMediator mediator,
			IUserRepository userRepository,
			IPaymentRepository paymentRepository,
			IHttpContextAccessorWrapper wrapper) : base(mediator, wrapper)
        {
			_userRepository = userRepository;
			_paymentRepository = paymentRepository;
        }

        [HttpPost]
        [Route("add-balance")]
		public async Task<ActionResult> AddBalance()
		{
			await Mediator.Send(new AddBalanceCommand { UserId = CurrentUserId });

			return NoContent();
		}
	}
}

