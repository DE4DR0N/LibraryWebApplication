using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryWebApp.Persistence
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var unitOfWork = services.GetRequiredService<IUnitOfWork>();

                await SeedAdminUserAsync(unitOfWork);
                await SeedAuthorsAsync(unitOfWork);
            }
        }

        private static async Task SeedAuthorsAsync(IUnitOfWork unitOfWork)
        {
            var authors = new List<AuthorEntity>
            {
                new AuthorEntity
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jane",
                    LastName = "Austen",
                    BirthDate = new DateOnly(1775, 12, 16),
                    Country = "United Kingdom"
                },
                new AuthorEntity
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Mark",
                    LastName = "Twain",
                    BirthDate = new DateOnly(1835, 11, 30),
                    Country = "United States"
                },
                new AuthorEntity
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Fyodor",
                    LastName = "Dostoevsky",
                    BirthDate = new DateOnly(1821, 11, 11),
                    Country = "Russia"
                }
            };

            foreach (var author in authors)
            {
                var existingAuthor = await unitOfWork.Authors.GetAuthorByNameAsync(author.FirstName, author.LastName);
                if (existingAuthor == null)
                {
                    await unitOfWork.Authors.AddAsync(author);
                }
            }

            await unitOfWork.CompleteAsync();
        }


        private static async Task SeedAdminUserAsync(IUnitOfWork unitOfWork)
        {
            const string adminName = "admin";
            var adminUser = new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = adminName,
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("Admin@123"),
                Role = "Admin"
            };

            var existingUser = await unitOfWork.Users.GetByUsernameAsync(adminUser.UserName);
            if (existingUser == null)
            {
                await unitOfWork.Users.AddAsync(adminUser);
                await unitOfWork.CompleteAsync();
            }
        }
    }
}
