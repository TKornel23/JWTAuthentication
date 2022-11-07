using Kornel.Domain;

namespace Kornel.Persistance
{
    public interface IAuthorizationService
    {
        Task<UserModel?> AuthenticateUser(UserModel login);
        Task<string> GenerateJSONWebToken(UserModel userInfo);
    }
}