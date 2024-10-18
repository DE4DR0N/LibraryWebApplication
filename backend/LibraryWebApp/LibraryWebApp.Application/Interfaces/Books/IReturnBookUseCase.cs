namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IReturnBookUseCase
    {
        Task ExecuteAsync(Guid bookId);
    }
}