using Realtea.Core.Enums;

namespace Realtea.Core.Entities
{
    public class Payment : BaseEntity
    {
        public int UserId { get; set; }

        public PaymentDetail PaymentDetail { get; set; }

        public DateTimeOffset PaymentMadeAt { get; set; }

        public decimal PaidAmount { get; set; }

        public int AdvertisementId { get; set; }
    }
}

