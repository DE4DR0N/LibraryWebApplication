using LibraryWebApp.Application.DTOs;
using LibraryWebApp.Application.DTOs.BookDTOs;
using LibraryWebApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;
        private readonly IImageService _imageService;
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", "Images");

        public BooksController(IBooksService booksService, IImageService imageService)
        {
            _booksService = booksService;
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery]PaginationViewModel model)
        {
            var books = await _booksService.GetAllBooksAsync(model);
            return Ok(books);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _booksService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost("addBook")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PostBook([FromBody] BookViewModel book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var image = await _imageService.CreateImageAsync(book.Image, _imagePath);
            var entity = await _booksService.AddBookAsync(book, image);
            return CreatedAtAction(nameof(GetBook), new { id = entity.Id}, book);
        }

        [HttpPut("updateBook/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutBook(Guid id, [FromBody] BookViewModel book)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            await _booksService.UpdateBookAsync(id, book);
            return NoContent();
        }

        [HttpDelete("deleteBook/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            await _booksService.DeleteBookAsync(id);
            return NoContent();
        }

        [HttpPost("issue")]
        [Authorize]
        public async Task<IActionResult> IssueBookToUser(IssueBookViewModel issueBookDto)
        {
            await _booksService.IssueBookToUserAsync(issueBookDto.BookId, issueBookDto.UserId, issueBookDto.ReturnDate);
            return Ok("Book issued to user");
        }

        [HttpPost("return")]
        [Authorize]
        public async Task<IActionResult> ReturnBook(Guid bookId)
        {
            await _booksService.ReturnBookAsync(bookId);
            return Ok("Book returned");
        }
    }
}
