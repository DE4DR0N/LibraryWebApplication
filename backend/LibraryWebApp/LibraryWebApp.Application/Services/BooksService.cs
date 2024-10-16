using AutoMapper;
using LibraryWebApp.Application.DTOs;
using LibraryWebApp.Application.DTOs.BookDTOs;
using LibraryWebApp.Application.Extensions;
using LibraryWebApp.Application.Interfaces;
using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;

namespace LibraryWebApp.Application.Services
{
    public class BooksService : IBooksService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BooksService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookResponseViewModel>> GetAllBooksAsync(PaginationViewModel model)
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            var pagedBooks = books.AsQueryable().ApplyPagination(model);
            return _mapper.Map<IEnumerable<BookResponseViewModel>>(pagedBooks);
        }

        public async Task<BookResponseViewModel> GetBookByIdAsync(Guid id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            return _mapper.Map<BookResponseViewModel>(book);
        }

        public async Task<BookResponseViewModel> GetBookByISBNAsync(long isbn)
        {
            var book = await _unitOfWork.Books.GetByIsbnAsync(isbn);
            return _mapper.Map<BookResponseViewModel>(book);
        }

        public async Task<BookResponseViewModel> AddBookAsync(BookViewModel bookDto, string image)
        {
            var book = _mapper.Map<BookEntity>(bookDto);
            book.Image = image;
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<BookResponseViewModel>(book);
        }

        public async Task UpdateBookAsync(Guid id, BookViewModel bookDto)
        {
            var book = _mapper.Map<BookEntity>(bookDto);
            book.Id = id;
            await _unitOfWork.Books.UpdateAsync(book);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteBookAsync(Guid id)
        {
            await _unitOfWork.Books.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task IssueBookToUserAsync(Guid bookId, Guid userId, DateOnly returnDate)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookId);
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            if (book.UserId != null)
            {
                throw new Exception("Book is already issued");
            }
            await _unitOfWork.Books.IssueBookToUserAsync(bookId, userId, DateOnly.FromDateTime(DateTime.Now), returnDate);
            await _unitOfWork.CompleteAsync();
        }

        public async Task ReturnBookAsync(Guid bookId)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookId);
            if (book == null)
            {
                throw new Exception("Book not found");
            }
            if (book.UserId == null)
            {
                throw new Exception("Book is already returned");
            }
            await _unitOfWork.Books.ReturnBookAsync(bookId);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<BookResponseViewModel>> GetBooksByUserAsync(Guid userId)
        {
            var books = await _unitOfWork.Books.GetBooksByUserAsync(userId);
            return _mapper.Map<IEnumerable<BookResponseViewModel>>(books);
        }
    }
}
