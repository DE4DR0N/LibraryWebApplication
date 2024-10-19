using AutoMapper;
using LibraryWebApp.Application.DTOs.BookDTOs;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Application.Interfaces.Images;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.UseCases.Books
{
    public class GetBookByIdUseCase : IGetBookByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGetImageUseCase _getImage;
        public GetBookByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper, IGetImageUseCase getImage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _getImage = getImage;
        }

        public async Task<IActionResult> ExecuteAsync(Guid id, string imagePath)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null) return new NotFoundObjectResult("Book not found");
            var imageKey = _getImage.ExecuteAsync(book.Image, imagePath);

            return new OkObjectResult(_mapper.Map<BookResponseViewModel>(book));
        }
    }
}
