using LibraryWebApp.Application.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.SignUp
{
    public interface IRegisterUseCase
    {
        Task<IActionResult> ExecuteAsync(RegisterViewModel model);
    }
}