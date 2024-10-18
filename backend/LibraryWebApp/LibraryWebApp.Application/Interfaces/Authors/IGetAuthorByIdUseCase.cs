using LibraryWebApp.Application.DTOs.AuthorDTOs;

namespace LibraryWebApp.Application.Interfaces.Authors
{
    public interface IGetAuthorByIdUseCase
    {
        Task<AuthorResponseViewModel> ExecuteAsync(Guid id);
    }
}