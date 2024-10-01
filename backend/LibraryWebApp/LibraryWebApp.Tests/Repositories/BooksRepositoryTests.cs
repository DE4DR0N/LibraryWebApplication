using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Persistence;
using LibraryWebApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Tests.Repositories
{
    public class BooksRepositoryTests
    {
        private BooksRepository _repository;
        private LibraryWebAppDbContext _context;

        public BooksRepositoryTests()
        {
            _context = TestDbContext.Create();
            _repository = new BooksRepository(_context);
        }

        [Fact]
        public async Task GetByIsbnAsync_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            long isbn = 123456789;

            // Act
            var result = await _repository.GetByIsbnAsync(isbn);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(isbn, result.ISBN);
        }

        [Fact]
        public async Task AddAsync_ShouldAddBookToDatabase()
        {
            var author = _context.Authors.First();
            // Arrange
            var newBook = new BookEntity
            {
                Id = Guid.NewGuid(),
                ISBN = 987654321,
                Title = "Title2",
                Genre = "Fantasy",
                Description = "Who moves fisrt is :)",
                AuthorId = _context.Authors.First().Id,
                Author = author
            };

            // Act
            await _repository.AddAsync(newBook);
            await _context.SaveChangesAsync();

            // Assert
            var bookFromDb = await _context.Books.FirstOrDefaultAsync(b => b.Id == newBook.Id);
            Assert.NotNull(bookFromDb);
            Assert.Equal(newBook.Title, bookFromDb.Title);
        }
    }
}