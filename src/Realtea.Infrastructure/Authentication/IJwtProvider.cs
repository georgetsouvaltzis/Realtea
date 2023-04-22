using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Realtea.Infrastructure.Identity;
using Realtea.Infrastructure.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Realtea.Infrastructure.Authentication
{
    public interface IJwtProvider
    {
        Task<string> GenerateAsync(ApplicationUser user);
    }

    public class JwtProvider : IJwtProvider
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        public JwtProvider(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> settings)
        {
            _userManager = userManager;
            _jwtSettings = settings.Value;
        }

        public async Task<string> GenerateAsync(ApplicationUser user)
        {
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)), SecurityAlgorithms.HmacSha256);

            var existingUser = await _userManager.FindByIdAsync(user.Id.ToString());
            var roles = await _userManager.GetRolesAsync(existingUser);
            var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenDurationInMinutes);
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("role", roles.First()),
                new Claim(JwtRegisteredClaimNames.Exp, expiresAt.ToString())
            };

            var token = new JwtSecurityToken(
                _jwtSettings.ValidIssuer,
                _jwtSettings.ValidAudience,
                claims,
                null,
                expiresAt,
                signingCredentials);

            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return generatedToken;
        }
    }
}

