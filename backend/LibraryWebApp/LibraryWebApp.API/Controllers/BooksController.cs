﻿using LibraryWebApp.Application.DTOs;
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
        private readonly IUrlHelper _urlHelper;
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", "Images");

        public BooksController(IGetAllBooksUseCase getAllBooksUseCase, IGetBookByIdUseCase getBookByIdUseCase, 
            IAddBookUseCase addBookUseCase, IUpdateBookUseCase updateBookUseCase, IDeleteBookUseCase deleteBookUseCase,
            IIssueBookUseCase issueBookUseCase, IReturnBookUseCase returnBookUseCase, 
            IGetImageUseCase getImageUseCase, ICreateImageUseCase createImageUseCase, IUrlHelper urlHelper)
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
            _urlHelper = urlHelper;
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
            return await _getBookById.ExecuteAsync(id, _imagePath, _urlHelper);
        }

        [HttpPost("addBook")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PostBook([FromForm] BookViewModel book)
        {
            var image = await _createImage.ExecuteAsync(book.Image, _imagePath);
            var entity = await _addBook.ExecuteAsync(book, image);
            return CreatedAtAction(nameof(GetBook), new { id = entity.Id}, book);
        }

        [HttpPut("updateBook/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutBook(Guid id, [FromBody] BookViewModel book)
        {            
            return await _updateBook.ExecuteAsync(id, book);
        }

        [HttpDelete("deleteBook/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            return await _deleteBook.ExecuteAsync(id);
        }

        [HttpPost("issue")]
        [Authorize]
        public async Task<IActionResult> IssueBookToUser(IssueBookViewModel issueBookDto)
        {
            return await _issueBook.ExecuteAsync(issueBookDto.BookId, issueBookDto.UserId, issueBookDto.ReturnDate);
        }

        [HttpPost("return")]
        [Authorize]
        public async Task<IActionResult> ReturnBook(Guid bookId)
        {
            return await _returnBook.ExecuteAsync(bookId);
        }
    }
}
