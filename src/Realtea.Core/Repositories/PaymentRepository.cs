using System;
using Realtea.Domain.Entities;
using Realtea.Infrastructure;

namespace Realtea.Core.Repositories
{
	public interface IPaymentRepository
	{
		Task CreateAsync(Payment payment);
	}
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RealTeaDbContext _db;

        public PaymentRepository(RealTeaDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task CreateAsync(Payment payment)
        {
            await _db.Payments.AddAsync(payment);
            await _db.SaveChangesAsync();
        }
    }
}

