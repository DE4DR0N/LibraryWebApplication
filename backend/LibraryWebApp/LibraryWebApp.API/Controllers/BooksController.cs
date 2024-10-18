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
        private readonly IGetImageUseCase _getImage;
        private readonly ICreateImageUseCase _createImage;
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", "Images");

        public BooksController(IGetAllBooksUseCase getAllBooksUseCase, IGetBookByIdUseCase getBookByIdUseCase, 
            IAddBookUseCase addBookUseCase, IUpdateBookUseCase updateBookUseCase, IDeleteBookUseCase deleteBookUseCase,
            IIssueBookUseCase issueBookUseCase, IReturnBookUseCase returnBookUseCase, 
            IGetImageUseCase getImageUseCase, ICreateImageUseCase createImageUseCase)
        {
            _getAllBooks = getAllBooksUseCase;
            _getBookById = getBookByIdUseCase;
            _addBook = addBookUseCase;
            _updateBook = updateBookUseCase;
            _deleteBook = deleteBookUseCase;
            _issueBook = issueBookUseCase;
            _returnBook = returnBookUseCase;
            _getImage = getImageUseCase;
            _createImage = createImageUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery]PaginationViewModel model)
        {
            var books = await _getAllBooks.ExecuteAsync(model);
            return Ok(books);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _getBookById.ExecuteAsync(id);
            if (book == null) return NotFound();

            var imageKey = _getImage.ExecuteAsync(book.Image, _imagePath);
            book.Image = Url.Content($"~/images/{imageKey}");
            return Ok(book);
        }

        [HttpPost("addBook")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PostBook([FromForm] BookViewModel book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var image = await _createImage.ExecuteAsync(book.Image, _imagePath);
            var entity = await _addBook.ExecuteAsync(book, image);
            return CreatedAtAction(nameof(GetBook), new { id = entity.Id}, book);
        }

        [HttpPut("updateBook/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutBook(Guid id, [FromBody] BookViewModel book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            await _updateBook.ExecuteAsync(id, book);
            return NoContent();
        }

        [HttpDelete("deleteBook/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            await _deleteBook.ExecuteAsync(id);
            return NoContent();
        }

        [HttpPost("issue")]
        [Authorize]
        public async Task<IActionResult> IssueBookToUser(IssueBookViewModel issueBookDto)
        {
            await _issueBook.ExecuteAsync(issueBookDto.BookId, issueBookDto.UserId, issueBookDto.ReturnDate);
            return Ok("Book issued to user");
        }

        [HttpPost("return")]
        [Authorize]
        public async Task<IActionResult> ReturnBook(Guid bookId)
        {
            await _returnBook.ExecuteAsync(bookId);
            return Ok("Book returned");
        }
    }
}
