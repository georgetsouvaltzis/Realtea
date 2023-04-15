//using System;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Realtea.App.Authorization;
//using Realtea.Core.Entities;
//using Realtea.Core.Enums;
//using Realtea.Core.Interfaces;
//using Realtea.Core.Interfaces.Services;
//using Realtea.Core.Repositories;
//using Realtea.Core.Services;
//using Realtea.Domain.Entities;
//using Realtea.Domain.Enums;

//namespace Realtea.App.Controllers.V1
//{

//    [Authorize]
//	public class PaymentsController : V1ControllerBase
//	{
//		//private readonly IPaymentRepository _paymentRepository;
//		private readonly IPaymentService _paymentService;
//		private readonly IAdvertisementService _advertisementService;
//		private readonly IAuthorizationService _authorizationService;
//		private readonly UserManager<User> _userManager;

//		public PaymentsController(IPaymentService paymentService, IAdvertisementService advertisementService,
//			IAuthorizationService authorizationService)
//		{
//			_paymentService = paymentService;
//			_advertisementService = advertisementService;
//			_authorizationService = authorizationService;
//		}

//		[Route("add-balance")]
//		[HttpPost]
//		public async Task<ActionResult> AddBalance([FromBody] decimal amount)
//		{
//			var userId = User.FindFirstValue("sub");
//			var existingUser = await _userManager.FindByIdAsync(userId);

//			if (existingUser == null)
//				throw new InvalidOperationException(nameof(existingUser));

//			_paymentService.CreateAsync();
//			await _paymentRepository.CreateAsync(new Payment
//			{
//				PaymentMadeAt = DateTimeOffset.UtcNow,
//				PaidAmount = amount,
//				PaymentDetail = PaymentDetail.Card,
//				UserId = Convert.ToInt32(userId),
//			});

//			return NoContent();
//		}


//		[HttpGet]
//		// Should retrieve user-related payment details
//		// Should be able to filter them out by having query params.
//		public async Task<ActionResult> GetPayments(int? userId, PaymentDetail? paymentDetail)
//		{

//			await _paymentService.GetPaymentsAsync();

//			var queryable = _paymentRepository.GetPaymentsQueryable();
//			if (userId != null)
//				queryable = queryable.Where(x => x.UserId == userId);

//			if (paymentDetail != null)
//				queryable = queryable.Where(x => x.PaymentDetail == paymentDetail);

//			return Ok(await queryable.ToListAsync());
//		}
//	}
//}

