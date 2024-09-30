using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Persistence.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly LibraryWebAppDbContext _context;
        public UsersRepository(LibraryWebAppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserEntity user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<IEnumerable<BookEntity>> GetBooksByUserAsync(Guid userId)
        {
            return await _context.Books.Where(b => b.UserId == userId).AsNoTracking().ToListAsync();
        }

        public async Task<UserEntity> GetByIdAsync(Guid id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<UserEntity> GetByUserNameAsync(string username)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(b => b.UserName == username);
        }

        public async Task UpdateAsync(UserEntity user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
