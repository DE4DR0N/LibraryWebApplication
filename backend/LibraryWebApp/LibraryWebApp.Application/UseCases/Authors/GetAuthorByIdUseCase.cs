using AutoMapper;
using LibraryWebApp.Application.DTOs.AuthorDTOs;
using LibraryWebApp.Application.Interfaces.Authors;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.UseCases.Authors
{
    public class GetAuthorByIdUseCase : IGetAuthorByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetAuthorByIdUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> ExecuteAsync(Guid id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            return new OkObjectResult(_mapper.Map<AuthorResponseViewModel>(author));
        }
    }
}
