using AutoMapper;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.UseCases.Books
{
    public class IssueBookUseCase : IIssueBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public IssueBookUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> ExecuteAsync(Guid bookId, Guid userId, DateOnly returnDate)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookId);
            if (book == null) return new NotFoundResult();
            if (book.UserId != null) return new BadRequestObjectResult("Book is already taken");
            book.UserId = userId;
            book.BorrowDate = DateOnly.FromDateTime(DateTime.Now);
            book.ReturnDate = returnDate;
            _unitOfWork.Books.Update(book);
            await _unitOfWork.CompleteAsync();
            return new OkObjectResult("Book issued");
        }
    }
}
