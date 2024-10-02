using LibraryWebApp.Domain.Entities;

namespace LibraryWebApp.Domain.Interfaces
{
    public interface IAuthorsRepository : IGenericRepository<AuthorEntity>
    {
        Task<IEnumerable<BookEntity>> GetBooksByAuthorAsync(Guid authorId);
        Task<AuthorEntity> GetAuthorByNameAsync(string firstname, string lastname);
    }
}