using AutoMapper;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.UseCases.Books
{
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteBookUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> ExecuteAsync(Guid id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null) return new NotFoundObjectResult("Book not found");
            _unitOfWork.Books.Delete(book);
            await _unitOfWork.CompleteAsync();
            return new NoContentResult();
        }
    }
}
