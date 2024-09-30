namespace LibraryWebApp.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBooksRepository Books { get; }
        IAuthorsRepository Authors { get; }
        IUsersRepository Users { get; }
        Task<int> CompleteAsync();
    }
}
