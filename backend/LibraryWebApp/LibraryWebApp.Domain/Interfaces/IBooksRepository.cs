using LibraryWebApp.Domain.Entities;

namespace LibraryWebApp.Domain.Interfaces
{
    public interface IBooksRepository : IGenericRepository<BookEntity>
    {
        Task<IEnumerable<BookEntity>> GetBooksByUserAsync(Guid userId);
        Task<IEnumerable<BookEntity>> GetBooksByAuthorAsync(Guid authorId);
        Task<BookEntity> GetByIsbnAsync(long isbn);
    }
}