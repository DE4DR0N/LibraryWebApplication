using AutoMapper;
using LibraryWebApp.Application.DTOs.AuthorDTOs;
using LibraryWebApp.Application.Interfaces.Authors;
using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;

namespace LibraryWebApp.Application.UseCases.Authors
{
    public class UpdateAuthorUseCase : IUpdateAuthorUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateAuthorUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task ExecuteAsync(Guid id, AuthorViewModel authorDto)
        {
            var updAuthor = _mapper.Map<AuthorEntity>(authorDto);
            updAuthor.Id = id;
            _unitOfWork.Authors.Update(updAuthor);
            await _unitOfWork.CompleteAsync();
        }
    }
}
