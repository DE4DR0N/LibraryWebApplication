using LibraryWebApp.Application.DTOs.AuthorDTOs;

namespace LibraryWebApp.Application.Interfaces.Authors
{
    public interface IAddAuthorUseCase
    {
        Task<AuthorResponseViewModel> ExecuteAsync(AuthorViewModel authorDto);
    }
}