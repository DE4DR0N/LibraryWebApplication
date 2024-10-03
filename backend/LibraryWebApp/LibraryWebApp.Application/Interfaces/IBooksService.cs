using LibraryWebApp.Application.DTOs;
using LibraryWebApp.Application.DTOs.BookDTOs;

namespace LibraryWebApp.Application.Interfaces
{
    public interface IBooksService
    {
        Task<BookResponseViewModel> AddBookAsync(BookViewModel bookDto);
        Task DeleteBookAsync(Guid id);
        Task<IEnumerable<BookResponseViewModel>> GetAllBooksAsync(PaginationViewModel paginationParams);
        Task<BookResponseViewModel> GetBookByIdAsync(Guid id);
        Task<BookResponseViewModel> GetBookByISBNAsync(long isbn);
        Task<IEnumerable<BookResponseViewModel>> GetBooksByUserAsync(Guid userId);
        Task UpdateBookAsync(Guid id, BookViewModel bookDto);
        Task IssueBookToUserAsync(Guid bookId, Guid userId, DateOnly returnDate);
        Task ReturnBookAsync(Guid bookId);
    }
}