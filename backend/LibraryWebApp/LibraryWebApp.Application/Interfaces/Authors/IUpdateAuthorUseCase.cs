using LibraryWebApp.Application.DTOs.AuthorDTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.Authors
{
    public interface IUpdateAuthorUseCase
    {
        Task<IActionResult> ExecuteAsync(Guid id, AuthorViewModel authorDto);
    }
}