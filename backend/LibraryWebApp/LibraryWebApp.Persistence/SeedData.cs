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
            }
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
