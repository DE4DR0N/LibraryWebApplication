using LibraryWebApp.Application.DTOs.AuthDTOs;

namespace LibraryWebApp.Application.Interfaces.SignUp
{
    public interface IRegisterUseCase
    {
        Task<bool> ExecuteAsync(RegisterViewModel model);
    }
}