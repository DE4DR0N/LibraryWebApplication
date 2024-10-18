using LibraryWebApp.Application.DTOs.BookDTOs;

namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IUpdateBookUseCase
    {
        Task ExecuteAsync(Guid id, BookViewModel bookDto);
    }
}