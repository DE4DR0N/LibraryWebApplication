using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Persistence.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryWebAppDbContext _context;
        public BooksRepository(LibraryWebAppDbContext context)
        {
            _context = context;
        }
        public async Task<BookEntity> GetByIsbnAsync(long isbn)
        {
            return await _context.Books.Include(b => b.Author).AsNoTracking().FirstOrDefaultAsync(a => a.ISBN == isbn);
        }
        public async Task<IEnumerable<BookEntity>> GetBooksByUserAsync(Guid userId)
        {
            return await _context.Books.Where(b => b.User.Id == userId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<BookEntity>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Author).AsNoTracking().ToListAsync();
        }
        public async Task<BookEntity> GetByIdAsync(Guid id)
        {
            return await _context.Books.Include(b => b.Author).AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<IEnumerable<BookEntity>> GetBooksByAuthorAsync(Guid authorId)
        {
            return await _context.Books.Where(b => b.Author.Id == authorId).AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(BookEntity book)
        {
            await _context.Books.AddAsync(book);
        }
        public void Update(BookEntity book)
        {
            _context.Books.Update(book);
        }
        public void Delete(BookEntity book)
        {
            _context.Books.Remove(book);
        }
    }
}
