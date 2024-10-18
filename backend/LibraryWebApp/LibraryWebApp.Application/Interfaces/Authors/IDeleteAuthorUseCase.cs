namespace LibraryWebApp.Application.Interfaces.Authors
{
    public interface IDeleteAuthorUseCase
    {
        Task ExecuteAsync(Guid id);
    }
}