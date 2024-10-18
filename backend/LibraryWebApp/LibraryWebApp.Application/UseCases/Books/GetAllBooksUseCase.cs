using AutoMapper;
using LibraryWebApp.Application.DTOs;
using LibraryWebApp.Application.DTOs.BookDTOs;
using LibraryWebApp.Application.Extensions;
using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Domain.Interfaces;

namespace LibraryWebApp.Application.UseCases.Books
{
    public class GetAllBooksUseCase : IGetAllBooksUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllBooksUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BookResponseViewModel>> ExecuteAsync(PaginationViewModel model)
        {
            var books = await _unitOfWork.Books.GetAllAsync();
            var pagedBooks = books.AsQueryable().ApplyPagination(model);
            return _mapper.Map<IEnumerable<BookResponseViewModel>>(pagedBooks);
        }
    }
}
