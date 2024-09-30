namespace LibraryWebApp.Application.DTOs
{
    public class ReturnBookViewModel
    {
        public Guid BookId { get; set; }
        public DateOnly ReturnDate { get; set; }
    }
}
