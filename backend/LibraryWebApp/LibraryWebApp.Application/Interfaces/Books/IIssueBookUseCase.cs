using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IIssueBookUseCase
    {
        Task<IActionResult> ExecuteAsync(Guid bookId, Guid userId, DateOnly returnDate);
    }
}