using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IGetBooksByUserUseCase
    {
        Task<IActionResult> ExecuteAsync(Guid userId);
    }
}