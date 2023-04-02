using Realtea.Domain.Enums;

namespace Realtea.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }

        public PaymentDetail PaymentDetail { get; set; }

        public DateTimeOffset PaymentMadeAt { get; set; }

        public decimal PaidAmount { get; set; }

        // For which Ad it was paid who.
        //public Advertisement Advertisement { get; set; }
        //public int AdvertisementId { get; set; }
    }
}
