namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IIssueBookUseCase
    {
        Task ExecuteAsync(Guid bookId, Guid userId, DateOnly returnDate);
    }
}