using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TP_FINAL_LABO_BACKEND.Models.User;

namespace TP_FINAL_LABO_BACKEND.Services
{
    public class AuthServices
    {
        private string secretKey;

        public AuthServices(IConfiguration config) 
        {
            secretKey = config.GetSection("jwtSettings").GetSection("secretKey").ToString() ?? null!;
        }

        public string GenerateJwtToken(User user) 
        {
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("id", user.UserId.ToString()));
            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    claims.AddClaim(new Claim(ClaimTypes.Role, role.NameRole));
                }
            }
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(tokenConfig);
            return token;
        }
    }
}
