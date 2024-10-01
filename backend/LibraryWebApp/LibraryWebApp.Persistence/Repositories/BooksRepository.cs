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
        public async Task<IEnumerable<BookEntity>> GetBooksByAuthorAsync(Guid authorId)
        {
            return await _context.Books.Where(b => b.AuthorId == authorId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<BookEntity>> GetBooksByUserAsync(Guid userId)
        {
            return await _context.Books.Where(b => b.UserId == userId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<BookEntity>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Author).AsNoTracking().ToListAsync();
        }
        public async Task<BookEntity> GetByIdAsync(Guid id)
        {
            return await _context.Books.Include(b => b.Author).AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task AddAsync(BookEntity book)
        {
            await _context.Books.AddAsync(book);
        }
        public async Task UpdateAsync(BookEntity book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }
        public async Task DeleteAsync(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null) _context.Books.Remove(book);
        }
        public async Task ReturnBookAsync(Guid bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                book.BorrowDate= null;
                book.ReturnDate = null;
                book.UserId = null;
                _context.Entry(book).State = EntityState.Modified;
            }
        }
        public async Task IssueBookToUserAsync(Guid bookId, Guid userId, DateOnly borrowDate, DateOnly returnDate)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                book.UserId = userId;
                book.BorrowDate = borrowDate;
                book.ReturnDate = returnDate;
                _context.Entry(book).State = EntityState.Modified;
            }
        }
    }
}
