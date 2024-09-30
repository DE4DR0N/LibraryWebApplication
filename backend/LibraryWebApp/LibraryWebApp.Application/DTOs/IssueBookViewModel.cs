namespace LibraryWebApp.Application.DTOs
{
    public class IssueBookViewModel
    {
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public DateOnly ReturnDate { get; set; }
    }
}
