using LibraryWebApp.Application.DTOs.AuthorDTOs;

namespace LibraryWebApp.Application.Interfaces
{
    public interface IAuthorsService
    {
        Task<AuthorResponseViewModel> AddAuthorAsync(AuthorViewModel authorDto);
        Task DeleteAuthorAsync(Guid id);
        Task<IEnumerable<AuthorResponseViewModel>> GetAllAuthorsAsync();
        Task<AuthorResponseViewModel> GetAuthorByIdAsync(Guid id);
        Task UpdateAuthorAsync(Guid id, AuthorViewModel authorDto);
    }
}