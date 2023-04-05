using System;
using Realtea.Core.Entities;

namespace Realtea.Core.Interfaces.Repositories
{
	public interface IPaymentRepository
	{
		Task CreateAsync(Payment payment);

		IQueryable<Payment> GetPaymentsQueryable();
	}
}
