using LibraryWebApp.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IGetAllBooksUseCase
    {
        Task<IActionResult> ExecuteAsync(PaginationViewModel model);
    }
}
