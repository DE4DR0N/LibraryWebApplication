using AutoMapper;
using LibraryWebApp.Application.DTOs.AuthorDTOs;
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

        public async Task<IEnumerable<AuthorResponseViewModel>> GetAllAuthorsAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorResponseViewModel>>(authors);
        }

        public async Task<AuthorResponseViewModel> GetAuthorByIdAsync(Guid id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            return _mapper.Map<AuthorResponseViewModel>(author);
        }

        public async Task<AuthorResponseViewModel> AddAuthorAsync(AuthorViewModel authorDto)
        {
            var author = _mapper.Map<AuthorEntity>(authorDto);
            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<AuthorResponseViewModel>(author);
        }

        public async Task UpdateAuthorAsync(Guid id, AuthorViewModel authorDto)
        {
            var updAuthor = _mapper.Map<AuthorEntity>(authorDto);
            updAuthor.Id = id;
            await _unitOfWork.Authors.UpdateAsync(updAuthor);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAuthorAsync(Guid id)
        {
            await _unitOfWork.Authors.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
