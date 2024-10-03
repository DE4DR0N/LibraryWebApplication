namespace LibraryWebApp.Domain.Entities
{
    public class BookEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required long ISBN { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public required string Description { get; set; }
        public Guid AuthorId { get; set; }
        public required AuthorEntity Author { get; set; }
        public Guid? UserId { get; set; }
        public UserEntity? User { get; set; }
        public DateOnly? BorrowDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
    }
}
