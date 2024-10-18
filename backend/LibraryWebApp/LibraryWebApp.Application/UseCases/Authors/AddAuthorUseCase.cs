using AutoMapper;
using LibraryWebApp.Application.DTOs.AuthorDTOs;
using LibraryWebApp.Application.Interfaces.Authors;
using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.UseCases.Authors
{
    public class AddAuthorUseCase : IAddAuthorUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddAuthorUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<AuthorResponseViewModel> ExecuteAsync(AuthorViewModel authorDto)
        {
            var author = _mapper.Map<AuthorEntity>(authorDto);
            await _unitOfWork.Authors.AddAsync(author);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<AuthorResponseViewModel>(author);
        }
    }
}
