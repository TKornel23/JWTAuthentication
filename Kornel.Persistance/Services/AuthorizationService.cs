using Kornel.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Kornel.Persistance
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IConfiguration configuration;
        public AuthorizationService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async  Task<UserModel?> AuthenticateUser(UserModel login)
        {
            if (login.UserName == "Jignesh")
            {
                return new UserModel { UserName = "Jignesh Trivedi", EmailAddress = "test.btest@gmail.com" };
            }
            return null;
        }

        public async Task<string> GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
                new Claim("DateOfJoing", userInfo.DateOfJoing.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
              issuer: configuration["Jwt:Issuer"],
              audience: configuration["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}