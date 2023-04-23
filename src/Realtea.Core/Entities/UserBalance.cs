using Realtea.Core.ValueObjects;

namespace Realtea.Core.Entities
{
    public record UserBalance : BaseEntity
    {
        private UserBalance() { }
        private UserBalance(int userId, Money balance)
        {
            UserId = userId;
            Balance = balance;
        }

        public static UserBalance Create(int userId, Money balance)
        {
            return new UserBalance(userId, balance);
        }

        public User User { get; private set; }
        public int UserId { get; private set; }

        public Money Balance { get; private set; }


        public bool IsCapableOfPayment()
        {
            return Balance.Value - 0.20m <= 0.0m;
        }

        public void UpdateBalance(Money toBeDeductedAmount)
        {
            var remainingAmount = Balance.Value - toBeDeductedAmount.Value;
            Balance = Money.Create(remainingAmount);
        }
    }
}

