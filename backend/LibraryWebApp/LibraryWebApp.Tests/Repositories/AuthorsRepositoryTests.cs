using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Persistence;
using LibraryWebApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApp.Tests.Repositories
{
    public class AuthorsRepositoryTests
    {
        private AuthorsRepository _repository;
        private LibraryWebAppDbContext _context;

        public AuthorsRepositoryTests()
        {
            _context = TestDbContext.Create();
            _repository = new AuthorsRepository(_context);
        }

        [Fact]
        public async Task GetByIdAsync()
        {
            // Arrange
            var author = _context.Authors.First();

            // Act
            var result = await _repository.GetByIdAsync(author.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(author.Id, result.Id);
        }
        [Fact]
        public async Task AddAsync()
        {
            // Arrange
            var newAuthor = new AuthorEntity { Id = Guid.NewGuid(), FirstName = "Test", LastName = "Test" };

            // Act
            await _repository.AddAsync(newAuthor);
            await _context.SaveChangesAsync();

            // Assert
            var authorDb = await _context.Authors.FirstOrDefaultAsync(a => a.Id == newAuthor.Id);
            Assert.NotNull(authorDb);
            Assert.Equal(newAuthor.FirstName, authorDb.FirstName);
            Assert.Equal(newAuthor.LastName, authorDb.LastName);
        }
    }
}