using AutoMapper;
using LibraryWebApp.Application.DTOs.BookDTOs;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Domain.Interfaces;

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
        public async Task<IEnumerable<BookResponseViewModel>> ExecuteAsync(Guid userId)
        {
            var books = await _unitOfWork.Books.GetBooksByUserAsync(userId);
            return _mapper.Map<IEnumerable<BookResponseViewModel>>(books);
        }
    }
}
