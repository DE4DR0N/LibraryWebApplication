using AutoMapper;
using LibraryWebApp.Application.DTOs;
using LibraryWebApp.Application.Interfaces;
using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;

namespace LibraryWebApp.Application.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AuthorViewModel>> GetAllAuthorsAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorViewModel>>(authors);
        }

        public async Task<AuthorViewModel> GetAuthorByIdAsync(Guid id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            return _mapper.Map<AuthorViewModel>(author);
        }

        public async Task AddAuthorAsync(AuthorViewModel authorDto)
        {
            var author = _mapper.Map<AuthorEntity>(authorDto);
            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAuthorAsync(AuthorViewModel authorDto)
        {
            var author = _mapper.Map<AuthorEntity>(authorDto);
            await _unitOfWork.Authors.UpdateAsync(author);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAuthorAsync(Guid id)
        {
            await _unitOfWork.Authors.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
