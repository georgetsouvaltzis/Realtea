using Realtea.Core.Enums;

namespace Realtea.Core.Entities
{
    //public class User : IdentityUser<int>
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public ICollection<Advertisement> Advertisements { get; set; }

        public UserType UserType { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public UserBalance UserBalance { get; set; }
        public int UserBalanceId { get; set; }
    }
}

