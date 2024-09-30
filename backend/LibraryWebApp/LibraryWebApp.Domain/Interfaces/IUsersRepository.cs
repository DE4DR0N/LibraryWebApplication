using LibraryWebApp.Domain.Entities;

namespace LibraryWebApp.Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<UserEntity> GetByIdAsync(Guid id);
        Task<UserEntity> GetByUserNameAsync(string email);
        Task AddAsync(UserEntity user);
        Task UpdateAsync(UserEntity user);
        Task<IEnumerable<BookEntity>> GetBooksByUserAsync(Guid userId);
    }
}
