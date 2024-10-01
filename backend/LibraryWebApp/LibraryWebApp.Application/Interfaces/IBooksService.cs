using LibraryWebApp.Application.DTOs;

namespace LibraryWebApp.Application.Interfaces
{
    public interface IBooksService
    {
        Task AddBookAsync(BookViewModel bookDto);
        Task DeleteBookAsync(Guid id);
        Task<IEnumerable<BookViewModel>> GetAllBooksAsync(PaginationViewModel paginationParams);
        Task<BookViewModel> GetBookByIdAsync(Guid id);
        Task<BookViewModel> GetBookByISBNAsync(long isbn);
        Task<IEnumerable<BookViewModel>> GetBooksByUserAsync(Guid userId);
        Task UpdateBookAsync(BookViewModel bookDto);
        Task IssueBookToUserAsync(Guid bookId, Guid userId, DateOnly returnDate);
        Task ReturnBookAsync(Guid bookId);
    }
}