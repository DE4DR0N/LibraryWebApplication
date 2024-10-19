using AutoMapper;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Application.Interfaces.Images;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.UseCases.Books
{
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGetImageUseCase _getImage;
        public DeleteBookUseCase(IUnitOfWork unitOfWork, IMapper mapper, IGetImageUseCase getImage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _getImage = getImage;
        }

        public async Task<IActionResult> ExecuteAsync(Guid id, string imagePath)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null) return new NotFoundObjectResult("Book not found");
            var imageKey = await _getImage.ExecuteAsync(book.Image, imagePath);
            if (!string.IsNullOrEmpty(imageKey))
            {
                var oldImagePath = Path.Combine(imagePath, book.Image);
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            _unitOfWork.Books.Delete(book);
            await _unitOfWork.CompleteAsync();
            return new NoContentResult();
        }
    }
}
