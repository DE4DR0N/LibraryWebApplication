using LibraryWebApp.Application.DTOs;
using LibraryWebApp.Application.DTOs.BookDTOs;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Application.Interfaces.Images;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IGetAllBooksUseCase _getAllBooks;
        private readonly IGetBookByIdUseCase _getBookById;
        private readonly IAddBookUseCase _addBook;
        private readonly IUpdateBookUseCase _updateBook;
        private readonly IDeleteBookUseCase _deleteBook;
        private readonly IIssueBookUseCase _issueBook;
        private readonly IReturnBookUseCase _returnBook;
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", "Images");

        public BooksController(IGetAllBooksUseCase getAllBooksUseCase, IGetBookByIdUseCase getBookByIdUseCase, 
            IAddBookUseCase addBookUseCase, IUpdateBookUseCase updateBookUseCase, IDeleteBookUseCase deleteBookUseCase,
            IIssueBookUseCase issueBookUseCase, IReturnBookUseCase returnBookUseCase)
        {
            _getAllBooks = getAllBooksUseCase;
            _getBookById = getBookByIdUseCase;
            _addBook = addBookUseCase;
            _updateBook = updateBookUseCase;
            _deleteBook = deleteBookUseCase;
            _issueBook = issueBookUseCase;
            _returnBook = returnBookUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery]PaginationViewModel model)
        {
            return await _getAllBooks.ExecuteAsync(model);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBook(Guid id)
        {
            return await _getBookById.ExecuteAsync(id, _imagePath);
        }

        [HttpPost("addBook")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PostBook([FromForm] BookViewModel book)
        {
            var entity = await _addBook.ExecuteAsync(book, _imagePath);
            return CreatedAtAction(nameof(GetBook), new { id = entity.Id}, book);
        }

        [HttpPut("updateBook/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutBook(Guid id, [FromForm] BookViewModel book)
        {            
            return await _updateBook.ExecuteAsync(id, book, _imagePath);
        }

        [HttpDelete("deleteBook/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            return await _deleteBook.ExecuteAsync(id, _imagePath);
        }

        [HttpPost("issue")]
        [Authorize]
        public async Task<IActionResult> IssueBookToUser(IssueBookViewModel issueBookDto)
        {
            return await _issueBook.ExecuteAsync(issueBookDto);
        }

        [HttpPost("return")]
        [Authorize]
        public async Task<IActionResult> ReturnBook(Guid bookId)
        {
            return await _returnBook.ExecuteAsync(bookId);
        }
    }
}
