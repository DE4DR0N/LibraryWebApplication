using LibraryWebApp.Application.DTOs;

namespace LibraryWebApp.Application.Interfaces
{
    public interface IUsersService
    {
        Task<UserViewModel> GetUserByUsernameAsync(string userName);
    }
}