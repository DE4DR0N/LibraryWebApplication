using AutoMapper;
using LibraryWebApp.Application.DTOs.BookDTOs;
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

        public async Task<IActionResult> ExecuteAsync(IssueBookViewModel model)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(model.BookId);
            if (book == null) return new NotFoundResult();
            if (book.UserId != null) return new BadRequestObjectResult("Book is already taken");
            book.UserId = model.UserId;
            book.BorrowDate = DateOnly.FromDateTime(DateTime.Now);
            book.ReturnDate = model.ReturnDate;
            _unitOfWork.Books.Update(book);
            await _unitOfWork.CompleteAsync();
            return new OkObjectResult("Book issued");
        }
    }
}
