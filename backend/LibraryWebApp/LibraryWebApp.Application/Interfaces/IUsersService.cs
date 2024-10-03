using LibraryWebApp.Application.DTOs.UserDTOs;

namespace LibraryWebApp.Application.Interfaces
{
    public interface IUsersService
    {
        Task<UserViewModel> GetUserByUsernameAsync(string userName);
    }
}