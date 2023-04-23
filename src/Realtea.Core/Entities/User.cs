using Realtea.Core.Enums;
using Realtea.Core.Exceptions;
using Realtea.Core.ValueObjects;

namespace Realtea.Core.Entities
{
    public record User : BaseEntity
    {
        private User(string firstName, string lastName, string userName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
        }

        public static User Create(string firstName, string lastName, string userName, string email)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ApiException(nameof(userName), FailureType.InvalidData);

            return new User(firstName, lastName, userName, email);
        }

        private readonly List<Advertisement> _advertisements = new List<Advertisement>();

        public IReadOnlyCollection<Advertisement> Advertisements => _advertisements;

        private readonly List<Payment> _payments = new List<Payment>();

        public string? FirstName { get; private set; }

        public string? LastName { get; private set; }

        public string? UserName { get; private set; }

        public string? Email { get; private set; }

        public UserBalance UserBalance { get; private set; }

        public void ChangeFirstName(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new ApiException(nameof(firstName), FailureType.InvalidData);

            FirstName = firstName;
        }

        public void ChangeLastName(string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                throw new ApiException(nameof(lastName), FailureType.InvalidData);

            LastName = lastName;
        }

        public void ChangeEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ApiException(nameof(email), FailureType.InvalidData);

            Email = email;
        }

        public void AddAd(Advertisement advertisement)
        {
            _advertisements.Add(advertisement);
        }

        public void RemoveAd(int advertisementId)
        {
            var existingAd = _advertisements.Find(x => x.Id == advertisementId);
            if (existingAd == null)
                throw new ApiException(nameof(existingAd), FailureType.Absent);

            _advertisements.Remove(existingAd);
        }

        public void DeductAmount()
        {
            UserBalance.UpdateBalance(Money.Create(0.2m));
        }
    }
}

