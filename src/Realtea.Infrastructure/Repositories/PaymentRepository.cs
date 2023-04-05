using System;
using Realtea.Core.Entities;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Core.Repositories;

namespace Realtea.Infrastructure.Repositories
{
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


        public IQueryable<Payment> GetPaymentsQueryable()
        {
            return _db.Payments.AsQueryable();
        }
    }
}

