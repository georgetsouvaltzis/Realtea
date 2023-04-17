using System;
using Realtea.Core.Entities;
using Realtea.Core.Interfaces.Repositories;


namespace Realtea.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RealTeaDbContext _db;

        public PaymentRepository(RealTeaDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<int> CreateAsync(Payment payment)
        {
            await _db.Payments.AddAsync(payment);
            await _db.SaveChangesAsync();

            return payment.Id;
        }

        public IQueryable<Payment> GetAsQueryable()
        {
            throw new NotImplementedException();
        }

        public Task GetByIdAsync(int id)
        {
            return Task.CompletedTask;
        }
    }
}

