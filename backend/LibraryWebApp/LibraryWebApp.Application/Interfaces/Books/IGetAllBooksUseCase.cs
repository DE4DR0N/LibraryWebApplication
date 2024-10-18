using LibraryWebApp.Application.DTOs.BookDTOs;
using LibraryWebApp.Application.DTOs;

namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IGetAllBooksUseCase
    {
        Task<IEnumerable<BookResponseViewModel>> ExecuteAsync(PaginationViewModel model);
    }
}
