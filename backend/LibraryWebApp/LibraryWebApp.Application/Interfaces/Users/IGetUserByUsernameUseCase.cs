using LibraryWebApp.Application.DTOs.UserDTOs;

namespace LibraryWebApp.Application.Interfaces.Users
{
    public interface IGetUserByUsernameUseCase
    {
        Task<UserViewModel> ExecuteAsync(string userName);
    }
}