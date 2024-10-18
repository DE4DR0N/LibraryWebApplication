using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IReturnBookUseCase
    {
        Task<IActionResult> ExecuteAsync(Guid bookId);
    }
}