using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.Users
{
    public interface IGetUserByUsernameUseCase
    {
        Task<IActionResult> ExecuteAsync(string userName);
    }
}