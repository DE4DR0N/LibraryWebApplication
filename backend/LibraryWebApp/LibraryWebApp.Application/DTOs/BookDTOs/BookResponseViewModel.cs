using LibraryWebApp.Application.DTOs.AuthorDTOs;

namespace LibraryWebApp.Application.DTOs.BookDTOs
{
    public class BookResponseViewModel
    {
        public Guid Id { get; set; }
        public required long ISBN { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public required string Description { get; set; }
        public required AuthorResponseViewModel Author { get; set; }
        public Guid? UserId { get; set; }
        public DateOnly? BorrowDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
    }
}
