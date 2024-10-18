using LibraryWebApp.Application.DTOs.AuthDTOs;

namespace LibraryWebApp.Application.Interfaces.SignIn
{
    public interface ILoginUseCase
    {
        Task<LoginResponseViewModel> ExecuteAsync(LoginViewModel model);
    }
}