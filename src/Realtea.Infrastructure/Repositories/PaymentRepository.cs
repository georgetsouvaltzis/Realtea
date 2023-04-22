using Realtea.Core.Entities;
using Realtea.Core.Interfaces.Repositories;

namespace Realtea.Infrastructure.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly RealTeaDbContext _db;
        private readonly IUserRepository _userRepository;

        public PaymentRepository(RealTeaDbContext dbContext, IUserRepository userRepository)
        {
            _db = dbContext;
            _userRepository = userRepository;
        }

        public async Task<int> CreateAsync(Payment payment)
        {
            var existingUser = await _userRepository.GetByIdAsync(payment.UserId.ToString());
            existingUser.UserBalance.Balance -= 0.20m;
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

