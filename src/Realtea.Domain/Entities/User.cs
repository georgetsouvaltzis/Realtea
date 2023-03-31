using Microsoft.AspNetCore.Identity;
using Realtea.Domain.Enums;

namespace Realtea.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public ICollection<Advertisement> Advertisements { get; set; }

        public UserType UserType { get; set; }
    }

    public class Payment : BaseEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }

        public PaymentDetail PaymentDetail { get; set; }

        public DateTimeOffset PaymentMadeAt { get; set; }

        public decimal PaidAmount { get; set; }

        // For which Ad it was paid who.
        public Advertisement Advertisement { get; set; }
        public int AdvertisementId { get; set; }
    }

    public enum PaymentDetail
    {
        Cash = 0,
        Card = 1
    }
}
