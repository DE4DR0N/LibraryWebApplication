using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.Authors
{
    public interface IDeleteAuthorUseCase
    {
        Task<IActionResult> ExecuteAsync(Guid id);
    }
}