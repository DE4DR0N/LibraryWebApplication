using AutoMapper;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.UseCases.Books
{
    public class ReturnBookUseCase : IReturnBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReturnBookUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> ExecuteAsync(Guid bookId)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(bookId);
            if (book == null) return new NotFoundObjectResult("Book not found");
            if (book.UserId == null) return new BadRequestObjectResult("Book is already returned");
            var user = await _unitOfWork.Users.GetByIdAsync((Guid)book.UserId);
            if (user == null) return new NotFoundObjectResult("User not found");
            book.BorrowDate = null;
            book.ReturnDate = null;
            book.UserId = null;
            _unitOfWork.Books.Update(book);
            await _unitOfWork.CompleteAsync();
            return new OkObjectResult("Book returned");
        }
    }
}
