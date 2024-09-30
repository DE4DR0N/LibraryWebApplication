using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Persistence;
using LibraryWebApp.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

public class BooksRepositoryTests
{
    private BooksRepository _repository;
    private LibraryWebAppDbContext _context;

    public BooksRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<LibraryWebAppDbContext>()
            .UseInMemoryDatabase(databaseName: "LibraryTestDb")
            .Options;

        _context = new LibraryWebAppDbContext(options);
        _repository = new BooksRepository(_context);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var author = new AuthorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "George",
            LastName = "Orwell",
            BirthDate = new DateOnly(1903, 6, 25)
        };

        var book = new BookEntity
        {
            Id = Guid.NewGuid(),
            ISBN = 123456789,
            Title = "1984",
            Genre = "Dystopian",
            Description = "A novel about a dystopian future.",
            Author = author,
            AuthorId = author.Id
        };

        _context.Authors.Add(author);
        _context.Books.Add(book);
        _context.SaveChanges();
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
            Title = "Animal Farm",
            Genre = "Political Satire",
            Description = "A story about a farm run by animals.",
            AuthorId = _context.Authors.First().Id,
            Author = author
        };

        // Act
        await _repository.AddAsync(newBook);
        await _context.SaveChangesAsync();

        // Assert
        var bookFromDb = await _context.Books.FirstOrDefaultAsync(b => b.ISBN == newBook.ISBN);
        Assert.NotNull(bookFromDb);
        Assert.Equal(newBook.Title, bookFromDb.Title);
    }
}
