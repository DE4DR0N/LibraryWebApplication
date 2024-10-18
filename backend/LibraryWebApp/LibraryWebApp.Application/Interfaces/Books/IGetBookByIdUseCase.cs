using LibraryWebApp.Application.DTOs.BookDTOs;

namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IGetBookByIdUseCase
    {
        Task<BookResponseViewModel> ExecuteAsync(Guid id);
    }
}
