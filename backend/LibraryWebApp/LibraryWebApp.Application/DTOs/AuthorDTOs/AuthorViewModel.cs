namespace LibraryWebApp.Application.DTOs.AuthorDTOs
{
    public record AuthorViewModel(
        string FirstName,
        string LastName,
        DateOnly BirthDate,
        string Country);
}
