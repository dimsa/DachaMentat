using DachaMentat.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace DachaMentat.Services
{
    public class UserAuthService : IUserAuthService
    {
        public string CreateToken(string login, string password)
        {            
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, login),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = AuthOptions.GetSymmetricSecurityKey();
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                                    issuer: AuthOptions.ISSUER,
                                    audience: AuthOptions.AUDIENCE,
                                   claims: claims,
                                   expires: DateTime.UtcNow.AddDays(1),
                                   signingCredentials: cred
   );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
