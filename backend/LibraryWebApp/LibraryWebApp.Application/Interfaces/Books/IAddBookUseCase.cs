using LibraryWebApp.Application.DTOs.BookDTOs;

namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IAddBookUseCase
    {
        Task<BookResponseViewModel> ExecuteAsync(BookViewModel bookDto, string image);
    }
}
