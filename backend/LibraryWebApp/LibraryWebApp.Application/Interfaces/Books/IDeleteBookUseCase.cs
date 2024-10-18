namespace LibraryWebApp.Application.Interfaces.Books
{
    public interface IDeleteBookUseCase
    {
        Task ExecuteAsync(Guid id);
    }
}