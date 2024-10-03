namespace LibraryWebApp.Application.DTOs.BookDTOs
{
    public class BookViewModel
    {
        public required long ISBN { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public required string Description { get; set; }
        public required Guid AuthorId { get; set; }
    }
}
