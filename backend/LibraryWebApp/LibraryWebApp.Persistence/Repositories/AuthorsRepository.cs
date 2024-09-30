using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Persistence.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly LibraryWebAppDbContext _context;

        public AuthorsRepository(LibraryWebAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookEntity>> GetBooksByAuthorAsync(Guid authorId)
        {
            return await _context.Books.Where(b => b.AuthorId == authorId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AuthorEntity>> GetAllAsync()
        {
            return await _context.Authors.AsNoTracking().ToListAsync();
        }

        public async Task<AuthorEntity> GetByIdAsync(Guid id)
        {
            return await _context.Authors.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(AuthorEntity author)
        {
            await _context.Authors.AddAsync(author);
        }

        public async Task UpdateAsync(AuthorEntity author)
        {
            _context.Entry(author).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null) _context.Authors.Remove(author);
        }
    }
}
