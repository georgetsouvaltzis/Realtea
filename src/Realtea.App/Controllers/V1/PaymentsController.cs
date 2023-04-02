using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Realtea.App.Authorization;
using Realtea.Core.Repositories;
using Realtea.Core.Services;
using Realtea.Domain.Entities;

namespace Realtea.App.Controllers.V1
{

    [Authorize]
	public class PaymentsController : V1ControllerBase
	{
		private readonly IPaymentRepository _paymentRepository;
		private readonly IAdvertisementService _advertisementService;
		private readonly IAuthorizationService _authorizationService;
		private readonly UserManager<User> _userManager;

		public PaymentsController(IPaymentRepository paymentRepository, IAdvertisementService advertisementService,
			IAuthorizationService authorizationService)
		{
			_paymentRepository = paymentRepository;
			_advertisementService = advertisementService;
			_authorizationService = authorizationService;
		}

		[Route("add-balance")]
		[HttpPost]
		public async Task<ActionResult> AddBalance([FromBody] decimal amount)
		{
			var userId = User.FindFirstValue("sub");
			var existingUser = await _userManager.FindByIdAsync(userId);

			if (existingUser == null)
				throw new InvalidOperationException(nameof(existingUser));

			await _paymentRepository.CreateAsync(new Payment
			{
				PaymentMadeAt = DateTimeOffset.UtcNow,
				PaidAmount = amount,
				PaymentDetail = Domain.Enums.PaymentDetail.Card,
				UserId = Convert.ToInt32(userId),
			});

			return NoContent();
		}


		// Should retrieve user-related payment details
		// Should be able to filter them out by having query params.
		public async Task<ActionResult> GetPayments()
		{
			return Ok();
		}
		//public async Task<ActionResult> MakePayment(int advertisementId)
		//{
		//	var existingAd = await _advertisementService.GetByIdAsync(advertisementId);

		//	if (existingAd == null)
		//		throw new InvalidOperationException(nameof(existingAd));

		//	var userId = User.FindFirstValue("sub");
		//	// TODO: Separate Requirement should be created for it.
		//	var result = await _authorizationService.AuthorizeAsync(User, existingAd, new IsEligibleForAdvertisementDeleteRequirement());

		//	if (!result.Succeeded)
		//		return Forbid();

		//	var now = DateTimeOffset.UtcNow;

		//	await _paymentRepository.CreateAsync(new Domain.Entities.Payment
		//	{
		//		AdvertisementId = advertisementId,
		//		PaymentMadeAt = DateTimeOffset.UtcNow,
		//		PaidAmount = 100.0m,
		//		PaymentDetail = Domain.Enums.PaymentDetail.Balance,
		//		UserId = Convert.ToInt32(userId),
		//	});

		//	return Ok();
		//}
	}
}

