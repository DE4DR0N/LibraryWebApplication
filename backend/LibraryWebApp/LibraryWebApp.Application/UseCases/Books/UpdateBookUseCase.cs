using AutoMapper;
using LibraryWebApp.Application.DTOs.BookDTOs;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Application.Interfaces.Images;
using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.UseCases.Books
{
    public class UpdateBookUseCase : IUpdateBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICreateImageUseCase _createImage;
        private readonly IGetImageUseCase _getImage;
        public UpdateBookUseCase(IUnitOfWork unitOfWork, IMapper mapper, ICreateImageUseCase createImage, IGetImageUseCase getImage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createImage = createImage;
            _getImage = getImage;
        }
        public async Task<IActionResult> ExecuteAsync(Guid id, BookViewModel model, string imagePath)
        {
            var oldBook = await _unitOfWork.Books.GetByIdAsync(id);
            if (oldBook == null) return new NotFoundObjectResult("Book not found");
            if (await _unitOfWork.Authors.GetByIdAsync(model.AuthorId) == null) return new NotFoundObjectResult("Author not found");
            if (await _unitOfWork.Books.GetByIsbnAsync(model.ISBN) != null) return new ConflictObjectResult("Book with such ISBN is already exist");

            var book = _mapper.Map<BookEntity>(model);

            var imageKey = await _getImage.ExecuteAsync(oldBook.Image, imagePath);
            if (!string.IsNullOrEmpty(imageKey))
            {
                var oldImagePath = Path.Combine(imagePath, oldBook.Image);
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            var image = await _createImage.ExecuteAsync(model.Image, imagePath, oldBook.Id);

            book.Id = id;
            book.Image = image;
            _unitOfWork.Books.Update(book);
            await _unitOfWork.CompleteAsync();
            return new NoContentResult();
        }
    }
}
