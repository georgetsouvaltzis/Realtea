using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Realtea.Domain.Entities;

namespace Realtea.Core.Services
{
	public interface IJwtProvider
	{
		string Generate(User user);
	}

    public class JwtProvider : IJwtProvider
    {
        public string Generate(User user)
        {
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("supersecretkey123")), SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
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

