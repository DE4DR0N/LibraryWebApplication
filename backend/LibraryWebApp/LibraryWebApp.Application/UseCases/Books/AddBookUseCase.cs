using AutoMapper;
using LibraryWebApp.Application.DTOs.BookDTOs;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;

namespace LibraryWebApp.Application.UseCases.Books
{
    public class AddBookUseCase : IAddBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddBookUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BookResponseViewModel> ExecuteAsync(BookViewModel bookDto, string image)
        {
            var book = _mapper.Map<BookEntity>(bookDto);
            book.Image = image;
            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<BookResponseViewModel>(book);
        }
    }
}
