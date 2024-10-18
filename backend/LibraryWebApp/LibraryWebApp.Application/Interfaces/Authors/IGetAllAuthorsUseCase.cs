using LibraryWebApp.Application.DTOs.AuthorDTOs;

namespace LibraryWebApp.Application.Interfaces.Authors
{
    public interface IGetAllAuthorsUseCase
    {
        Task<IEnumerable<AuthorResponseViewModel>> ExecuteAsync();
    }
}