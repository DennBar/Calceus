using Calceus.Shared;

namespace Calceus.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>?> Register(UserRegister user);
    }
}
