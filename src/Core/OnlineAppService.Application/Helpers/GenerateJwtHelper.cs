using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineAppService.Domain.Entities;
using OnlineAppService.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineAppService.Application.Helpers
{
    public class GenerateJwtHelper
    {
        private IConfiguration _configuration { get; set; }

        public GenerateJwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JwtKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                        new Claim(JWTUser.ID, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.UserType.ToString()),
                    }),
                Issuer = "false",
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.Token;
        }
    }
}
