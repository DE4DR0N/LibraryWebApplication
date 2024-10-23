namespace LibraryWebApp.Application.DTOs.BookDTOs
{
    public record IssueBookViewModel(
        Guid BookId,
        Guid UserId,
        DateOnly ReturnDate);
}
