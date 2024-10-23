namespace LibraryWebApp.Application.DTOs.AuthorDTOs
{
    public record AuthorResponseViewModel(Guid Id, string FirstName, string LastName, DateOnly BirthDate, string? Country);
}
