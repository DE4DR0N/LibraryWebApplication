using LibraryWebApp.Domain.Interfaces;
using LibraryWebApp.Persistence.Repositories;

namespace LibraryWebApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryWebAppDbContext _context;
        private BooksRepository _booksRepository;
        private AuthorsRepository _authorsRepository;
        private UsersRepository _usersRepository;

        public UnitOfWork(LibraryWebAppDbContext context)
        {
            _context = context;
        }

        public IBooksRepository Books => _booksRepository ??= new BooksRepository(_context);
        public IAuthorsRepository Authors => _authorsRepository ??= new AuthorsRepository(_context);
        public IUsersRepository Users => _usersRepository ??= new UsersRepository(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
