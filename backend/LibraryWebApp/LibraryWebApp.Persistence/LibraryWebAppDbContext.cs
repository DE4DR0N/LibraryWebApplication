using LibraryWebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Persistence
{
    public class LibraryWebAppDbContext : DbContext
    {
        public LibraryWebAppDbContext(DbContextOptions<LibraryWebAppDbContext> options)
            : base(options) { }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<UserEntity> Users {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookEntity>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(b => b.ISBN).IsRequired();
                entity.HasIndex(b => b.ISBN).IsUnique();
                entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
                entity.Property(b => b.Genre).IsRequired().HasMaxLength(100);
                entity.Property(b => b.Description).HasMaxLength(1000);

                entity.HasOne(b => b.Author)
                      .WithMany(a => a.Books)
                      .HasForeignKey(b => b.AuthorId);

                entity.HasOne(b => b.User)
                      .WithMany(u => u.Books)
                      .HasForeignKey(b => b.UserId);
            });

            modelBuilder.Entity<AuthorEntity>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(a => a.LastName).IsRequired().HasMaxLength(100);
                entity.Property(a => a.BirthDate).IsRequired();
                entity.Property(a => a.Country).HasMaxLength(100);
            });

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.UserName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.Role).IsRequired();
                entity.HasIndex(u => u.UserName).IsUnique();
            });
        }
    }
}
