using StoreDeLaCruz.Core.Aplication.DTOs.Account;

namespace StoreDeLaCruz.Core.Aplication.Interfaces.Service
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticationAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task SignOutAsync();
    }
}