namespace LibraryWebApp.Application.DTOs
{
    public class BookViewModel
    {
        public Guid Id { get; set; }
        public required long ISBN { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public required string Description { get; set; }
        public required Guid AuthorId { get; set; }
        //public AuthorViewModel Author { get; set; }
        public Guid? UserId { get; set; }
        public DateOnly? BorrowDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
    }
}
