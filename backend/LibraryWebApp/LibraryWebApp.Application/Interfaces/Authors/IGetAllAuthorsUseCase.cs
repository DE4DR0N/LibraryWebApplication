using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.Authors
{
    public interface IGetAllAuthorsUseCase
    {
        Task<IActionResult> ExecuteAsync();
    }
}