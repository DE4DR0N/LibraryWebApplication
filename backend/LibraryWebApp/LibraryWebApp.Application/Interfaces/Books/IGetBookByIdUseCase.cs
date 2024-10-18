using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IGetBookByIdUseCase
    {
        Task<IActionResult> ExecuteAsync(Guid id, string imagePath, IUrlHelper urlHelper);
    }
}
