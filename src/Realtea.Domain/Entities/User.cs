using Microsoft.AspNetCore.Identity;
using Realtea.Domain.Enums;

namespace Realtea.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public ICollection<Advertisement> Advertisements { get; set; }

        public UserType UserType { get; set; }
    }
}
