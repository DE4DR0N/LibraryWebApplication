using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IDeleteBookUseCase
    {
        Task<IActionResult> ExecuteAsync(Guid id);
    }
}