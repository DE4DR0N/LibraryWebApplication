using Microsoft.AspNetCore.Http;

namespace LibraryWebApp.Application.DTOs.BookDTOs
{
    public record BookViewModel(
        long ISBN,
        string Title,
        string Genre,
        string Description,
        Guid AuthorId,
        IFormFile Image);
}
