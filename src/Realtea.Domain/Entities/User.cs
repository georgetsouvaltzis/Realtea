using Microsoft.AspNetCore.Identity;
using Realtea.Domain.Enums;

namespace Realtea.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public ICollection<Advertisement> Advertisements { get; set; }

        public UserType UserType { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public UserBalance UserBalance { get; set; }
        public int UserBalanceId { get; set; }
    }

    public class UserBalance
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public decimal Balance { get; set; }

        public bool IsCapableOfPayment => Balance - 0.20m >= 0.0m;
    }
}
