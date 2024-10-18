using LibraryWebApp.Application.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.SignIn
{
    public interface ILoginUseCase
    {
        Task<IActionResult> ExecuteAsync(LoginViewModel model, HttpContext httpContext);
    }
}