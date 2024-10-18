using LibraryWebApp.Domain.Entities;

namespace LibraryWebApp.Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<UserEntity> GetByIdAsync(Guid id);
        Task<UserEntity> GetByUsernameAsync(string email);
        Task AddAsync(UserEntity user);
        void Update(UserEntity user);
    }
}
