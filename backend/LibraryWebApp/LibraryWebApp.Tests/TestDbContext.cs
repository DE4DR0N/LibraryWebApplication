using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Tests
{
    public class TestDbContext
    {
        public static LibraryWebAppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<LibraryWebAppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new LibraryWebAppDbContext(options);
            context.Database.EnsureCreated();

            var author = new AuthorEntity
            {
                Id = Guid.NewGuid(),
                FirstName = "AuthorName",
                LastName = "AuthorSurname",
                BirthDate = new DateOnly(2000, 01, 01),
                Country = "Country"
            };
            var book = new BookEntity
            {
                Id = Guid.NewGuid(),
                ISBN = 123456789,
                Title = "Book1",
                Genre = "Fantasy",
                Description = "English or spanish?",
                Author = author,
                AuthorId = author.Id,
                Image = "string"
            };
            context.Authors.Add(author);
            context.Books.Add(book);
            context.SaveChanges();

            return context;
        }
    }
}
