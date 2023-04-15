

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Realtea.Core.Entities;
using Realtea.Infrastructure.Identity;

namespace Realtea.Infrastructure.Authentication
{
    public interface IJwtProvider
    {
        string Generate(ApplicationUser user);
    }

    public class JwtProvider : IJwtProvider
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public JwtProvider(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
    
        /// TODO: When user updates their account type, they should get updated data.   
        public string Generate(ApplicationUser user)
        {
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkey123")), SecurityAlgorithms.HmacSha256);

            var role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().Single();
            var claims = new Claim[]
            {            
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(type: "role", role)
            };

            var token = new JwtSecurityToken(
                "iss",
                "aud",
                claims,
                null,
                DateTime.Now.AddHours(1),
                signingCredentials);

            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return generatedToken;
        }
    }
}

