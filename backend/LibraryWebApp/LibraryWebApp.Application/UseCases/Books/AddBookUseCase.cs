using AutoMapper;
using LibraryWebApp.Application.DTOs.BookDTOs;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Application.Interfaces.Images;
using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;

namespace LibraryWebApp.Application.UseCases.Books
{
    public class AddBookUseCase : IAddBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICreateImageUseCase _createImage;
        public AddBookUseCase(IUnitOfWork unitOfWork, IMapper mapper, ICreateImageUseCase createImage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createImage = createImage;
        }
        public async Task<BookResponseViewModel> ExecuteAsync(BookViewModel model, string imagePath)
        {
            if (await _unitOfWork.Books.GetByIsbnAsync(model.ISBN) != null) throw new Exception("Conflict ISBN");
            if (await _unitOfWork.Authors.GetByIdAsync(model.AuthorId) == null) throw new KeyNotFoundException("Author");
            var book = _mapper.Map<BookEntity>(model);
            var image = await _createImage.ExecuteAsync(model.Image, imagePath, book.Id);

            book.Image = image;
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<BookResponseViewModel>(book);
        }
    }
}
