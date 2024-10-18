using AutoMapper;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Domain.Interfaces;

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

        public async Task ExecuteAsync(Guid bookId, Guid userId, DateOnly returnDate)
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
    }
}
