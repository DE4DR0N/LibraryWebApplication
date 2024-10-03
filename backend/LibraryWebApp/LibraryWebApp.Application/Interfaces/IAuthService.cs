using LibraryWebApp.Application.DTOs.AuthDTOs;

namespace LibraryWebApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterViewModel model);
        Task<LoginResponseViewModel> LoginAsync(LoginViewModel model);
        Task<LoginResponseViewModel> RefreshTokenAsync(RefreshTokenViewModel refreshToken);
    }
}
