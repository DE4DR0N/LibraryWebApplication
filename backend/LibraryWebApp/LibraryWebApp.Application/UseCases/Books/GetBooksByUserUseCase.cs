using AutoMapper;
using LibraryWebApp.Application.DTOs.BookDTOs;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.UseCases.Books
{
    public class GetBooksByUserUseCase : IGetBooksByUserUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBooksByUserUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> ExecuteAsync(Guid userId)
        {
            if (await _unitOfWork.Users.GetByIdAsync(userId) == null) return new NotFoundObjectResult("User not found");
            var books = await _unitOfWork.Books.GetBooksByUserAsync(userId);
            if (books == null) return new NotFoundObjectResult("No books are borrowed");
            return new OkObjectResult(_mapper.Map<IEnumerable<BookResponseViewModel>>(books));
        }
    }
}
