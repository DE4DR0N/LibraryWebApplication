using LibraryWebApp.Application.DTOs;

namespace LibraryWebApp.Application.Interfaces
{
    public interface IAuthorsService
    {
        Task AddAuthorAsync(AuthorViewModel authorDto);
        Task DeleteAuthorAsync(Guid id);
        Task<IEnumerable<AuthorViewModel>> GetAllAuthorsAsync();
        Task<AuthorViewModel> GetAuthorByIdAsync(Guid id);
        Task UpdateAuthorAsync(AuthorViewModel authorDto);
    }
}