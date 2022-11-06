using Kornel.Persistance;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Net;

namespace Kornel.Test
{
    public class AuthenticationTests
    {
        [Theory]
        [InlineData("Jignesh", "password", 200)]
        [InlineData("userName", "password", 401)]
        public async Task CorrectCredentials_Return_CorrectStatusCode(
            string userName,
            string password,
            int statusCode)
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                { "Jwt:Issuer", "localhost.com" },
                { "Jwt:Key", "RgUkXp2s5v8y/A?D(G+KbPeShVmYq3t6" }
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var authorizationService = new AuthorizationService(configuration);

            var loginController = new LoginController(authorizationService);

            var response = await loginController.Login(new Domain.UserModel() { UserName = userName, Password = password });

            var result = response?
                            .GetType()?
                            .GetProperty("StatusCode")?
                            .GetValue(response, null);

            Assert.Equal(statusCode, result);
        }
    }
}