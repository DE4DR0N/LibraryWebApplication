using LibraryWebApp.Application.DTOs.AuthorDTOs;

namespace LibraryWebApp.Application.Interfaces.Authors
{
    public interface IUpdateAuthorUseCase
    {
        Task ExecuteAsync(Guid id, AuthorViewModel authorDto);
    }
}