using AutoMapper;
using LibraryWebApp.Application.DTOs;
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

        public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync(PaginationViewModel model)
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            var pagedBooks = books.AsQueryable().ApplyPagination(model);
            return _mapper.Map<IEnumerable<BookViewModel>>(pagedBooks);
        }

        public async Task<BookViewModel> GetBookByIdAsync(Guid id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            return _mapper.Map<BookViewModel>(book);
        }

        public async Task<BookViewModel> GetBookByISBNAsync(long isbn)
        {
            var book = await _unitOfWork.Books.GetByIsbnAsync(isbn);
            return _mapper.Map<BookViewModel>(book);
        }

        public async Task AddBookAsync(BookViewModel bookDto)
        {
            var book = _mapper.Map<BookEntity>(bookDto);
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateBookAsync(BookViewModel bookDto)
        {
            var book = _mapper.Map<BookEntity>(bookDto);
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

            await _unitOfWork.Books.ReturnBookAsync(bookId);
            await _unitOfWork.CompleteAsync();
        }
    }
}
