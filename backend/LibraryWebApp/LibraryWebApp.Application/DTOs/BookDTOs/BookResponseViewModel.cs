using LibraryWebApp.Application.DTOs.AuthorDTOs;

namespace LibraryWebApp.Application.DTOs.BookDTOs
{
    public record BookResponseViewModel(Guid Id, long ISBN, string Title, string Genre, string Description,
        AuthorResponseViewModel Author, Guid? UserId, DateOnly? BorrowDate, DateOnly? ReturnDate, string? Image);
}
