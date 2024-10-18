using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.Authors
{
    public interface IGetAuthorByIdUseCase
    {
        Task<IActionResult> ExecuteAsync(Guid id);
    }
}