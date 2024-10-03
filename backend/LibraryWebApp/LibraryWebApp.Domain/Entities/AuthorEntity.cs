namespace LibraryWebApp.Domain.Entities
{
    public class AuthorEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly BirthDate { get; set; }
        public required string Country { get; set; }
        public ICollection<BookEntity>? Books { get; set; }
    }
}
