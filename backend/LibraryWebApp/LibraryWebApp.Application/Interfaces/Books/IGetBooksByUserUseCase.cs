using LibraryWebApp.Application.DTOs.BookDTOs;

namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IGetBooksByUserUseCase
    {
        Task<IEnumerable<BookResponseViewModel>> ExecuteAsync(Guid userId);
    }
}