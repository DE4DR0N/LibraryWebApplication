using LibraryWebApp.Application.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.SignIn
{
    public interface IRefreshTokenUseCase
    {
        Task<IActionResult> ExecuteAsync(RefreshTokenViewModel model);
    }
}