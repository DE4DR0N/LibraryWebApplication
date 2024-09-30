using LibraryWebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Tests
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        { }

        public DbSet<BookEntity> Books { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<UserEntity> Users { get; set; }

    }
}
