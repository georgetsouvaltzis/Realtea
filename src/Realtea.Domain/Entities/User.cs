using Microsoft.AspNetCore.Identity;

namespace Realtea.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public ICollection<Advertisement> Advertisements { get; set; }
    }
}
