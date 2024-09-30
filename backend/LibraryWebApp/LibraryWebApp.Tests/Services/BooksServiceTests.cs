using AutoMapper;
using LibraryWebApp.Application.DTOs;
using LibraryWebApp.Application.Services;
using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;
using Moq;
public class BooksServiceTests
{
    private readonly Mock<IBooksRepository> _booksRepositoryMock;
    private readonly BooksService _booksService;
    private readonly IMapper _mapper;

    public BooksServiceTests()
    {
        _booksRepositoryMock = new Mock<IBooksRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<BookEntity, BookViewModel>().ReverseMap();
        });

        _mapper = config.CreateMapper();
        _booksService = new BooksService(new Mock<IUnitOfWork>().Object, _mapper);
    }
    [Fact]
    public async Task GetBookByIsbnAsync_ShouldReturnBookViewModel_WhenBookExists()
    {
        var author = new AuthorEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "George",
            LastName = "Orwell",
            BirthDate = new DateOnly(1903, 6, 25)
        };
        const long isbn = 123456789;
        var bookEntity = new BookEntity
        {
            Id = Guid.NewGuid(),
            ISBN = isbn,
            Title = "1984",
            Genre = "Dystopian",
            Description = "A novel about a dystopian future.",
            Author = author,
            AuthorId = author.Id
        };

        _booksRepositoryMock.Setup(repo => repo.GetByIsbnAsync(isbn))
            .ReturnsAsync(bookEntity);

        var result = await _booksService.GetBookByISBNAsync(isbn);

        Assert.NotNull(result);
        Assert.Equal(isbn, result.ISBN);
        Assert.Equal("1984", result.Title);
    }


    [Fact]
    public async Task AddBookAsync_ShouldCallAddMethodOfRepository()
    {
        // Arrange
        var bookViewModel = new BookViewModel
        {
            ISBN = 987654321,
            Title = "Animal Farm",
            Genre = "Political Satire",
            Description = "A story about a farm run by animals.",
            AuthorId = Guid.NewGuid()
        };

        // Act
        await _booksService.AddBookAsync(bookViewModel);

        // Assert
        _booksRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<BookEntity>()), Times.Once);
    }
}