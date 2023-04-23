using Realtea.Core.Enums;
using Realtea.Core.Exceptions;
using Realtea.Core.ValueObjects;

namespace Realtea.Core.Entities
{
    public record class Payment : BaseEntity
    {
        private const int DefaultValue = 0;

        private Payment() { }

        private Payment(int userId, PaymentDetail paymentDetail, DateTimeOffset paymentMadeAt, int advertisementId, decimal paidAmount)
        {
            UserId = userId;
            PaymentDetail = paymentDetail;
            PaymentMadeAt = paymentMadeAt;
            AdvertisementId = advertisementId;
            PaidAmount = paidAmount;
        }

        public static Payment Create(int userId, PaymentDetail paymentDetail, DateTimeOffset paymentMadeAt, int advertisementId, Money paidAmount)
        {
            if (userId == DefaultValue)
                throw new ApiException(nameof(userId), FailureType.InvalidData);

            if (advertisementId == DefaultValue)
                throw new ApiException(nameof(advertisementId), FailureType.InvalidData);

            return new Payment(userId, paymentDetail, paymentMadeAt, advertisementId, paidAmount.Value);
        }

        public int UserId { get; }

        public PaymentDetail PaymentDetail { get; }

        public DateTimeOffset PaymentMadeAt { get; }

        public decimal PaidAmount { get; }

        public int AdvertisementId { get; }
    }
}

