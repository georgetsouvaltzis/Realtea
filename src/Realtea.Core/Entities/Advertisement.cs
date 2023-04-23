using Realtea.Core.Enums;
using Realtea.Core.Exceptions;
using Realtea.Core.ValueObjects;

namespace Realtea.Core.Entities
{
    public record Advertisement : BaseEntity
	{
        private Advertisement() { }

        private Advertisement(string name,
            string description,
            AdvertisementType adType,
            bool isActive,
            int userId,
            DealType dealType,
            Money price,
            Sq2 squareMeter,
            Location location)
        {
            Name = name;
            Description = description;
            AdvertisementType = adType;
            IsActive = isActive;
            UserId = userId;
            DealType = dealType;
            Price = price;
            SquareMeter = squareMeter;
            Location = location;
        }

        public static Advertisement Create(string name,
            string description,
            AdvertisementType adType,
            bool isActive,
            int userId,
            DealType dealType,
            Money price,
            Sq2 squareMeter,
            Location location)
        {
            if (string.IsNullOrEmpty(name))
                throw new ApiException(nameof(name), FailureType.InvalidData);

            if (string.IsNullOrEmpty(description))
                throw new ApiException(nameof(description), FailureType.InvalidData);
                

            return new Advertisement(name, description, adType, isActive, userId, dealType, price, squareMeter, location);
        }


        public string Name { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public AdvertisementType AdvertisementType { get; private set; } = AdvertisementType.Free;

        public bool IsActive { get; private set; }

        public User User { get; private set; }

        public int UserId { get; private set; }

        public DealType DealType { get; private set; }

        public Money Price { get; private set; }

        public Sq2 SquareMeter { get; private set; }

        public Location Location { get; private set; }

        public void SetIsActive(bool value)
        {
            IsActive = value;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ApiException(nameof(name), FailureType.InvalidData);

            Name = name;
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ApiException(nameof(description), FailureType.InvalidData);

            Description = description;
        }

        public void ChangeDealType(DealType dealType)
        {
            DealType = dealType;
        }

        public void ChangeLocation(Location location)
        {
            Location = location;
        }

        public void ChangePrice(Money price)
        {
            Price = price;
        }

        public void ChangeSq2(Sq2 sq2)
        {
            SquareMeter = sq2;
        }
    }
}