namespace Realtea.Core.Entities
{
    public class UserBalance
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public decimal Balance { get; set; }

        public bool IsCapableOfPayment => Balance - 0.20m >= 0.0m;
    }
}

