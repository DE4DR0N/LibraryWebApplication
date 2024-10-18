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

        public async Task<UserEntity> GetByIdAsync(Guid id)
        {
            return await _context.Users.Include(b => b.Books).AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<UserEntity> GetByUsernameAsync(string username)
        {
            return await _context.Users.Include(b => b.Books).AsNoTracking().FirstOrDefaultAsync(b => b.UserName == username);
        }

        public void Update(UserEntity user)
        {
            _context.Users.Update(user);
        }
    }
}
