using LibraryWebApp.Application.DTOs.AuthDTOs;

namespace LibraryWebApp.Application.Interfaces.SignIn
{
    public interface IRefreshTokenUseCase
    {
        Task<LoginResponseViewModel> ExecuteAsync(RefreshTokenViewModel model);
    }
}