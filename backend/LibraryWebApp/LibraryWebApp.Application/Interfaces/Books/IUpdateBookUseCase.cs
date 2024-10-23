using LibraryWebApp.Application.DTOs.BookDTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IUpdateBookUseCase
    {
        Task<IActionResult> ExecuteAsync(Guid id, BookViewModel bookDto, string imagePath);
    }
}