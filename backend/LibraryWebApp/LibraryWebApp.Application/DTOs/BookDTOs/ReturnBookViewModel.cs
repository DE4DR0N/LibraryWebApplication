namespace LibraryWebApp.Application.DTOs.BookDTOs
{
    public class ReturnBookViewModel
    {
        public Guid BookId { get; set; }
        public DateOnly ReturnDate { get; set; }
    }
}
