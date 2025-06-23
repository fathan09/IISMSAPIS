using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using IISMSBackend.Entities;

namespace IISMSBackend.Support

{
    public static class JwtHelper 
    {
        public static string GenerateJwtToken(User user) 
        {
            var key = Encoding.UTF8.GetBytes("b!$p@JvXy$GzW8qLzC!kM4dV3mFzN0vY"); 
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.email),
                    new Claim(ClaimTypes.Role, user.role)
                }),
                Issuer = "fathan", 
                Audience = "precisioncultivations",
                Expires = DateTime.UtcNow.AddMonths(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
