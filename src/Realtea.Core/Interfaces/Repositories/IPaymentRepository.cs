using System;
using Realtea.Core.Entities;

namespace Realtea.Core.Interfaces.Repositories
{
	public interface IPaymentRepository
	{
		Task<int> CreateAsync(Payment payment);

		IQueryable<Payment> GetAsQueryable();

		Task GetByIdAsync(int id);
	}
}
