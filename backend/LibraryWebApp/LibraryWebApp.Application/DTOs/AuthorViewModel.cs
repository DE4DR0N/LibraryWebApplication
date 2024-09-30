namespace LibraryWebApp.Application.DTOs
{
    public class AuthorViewModel
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? Country { get; set; }
    }
}
