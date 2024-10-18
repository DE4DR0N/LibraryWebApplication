using AutoMapper;
using LibraryWebApp.Application.DTOs.AuthorDTOs;
using LibraryWebApp.Application.Interfaces.Authors;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.UseCases.Authors
{
    public class GetAllAuthorsUseCase : IGetAllAuthorsUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetAllAuthorsUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> ExecuteAsync()
        {
            var authors = await _unitOfWork.Authors.GetAllAsync();
            return new OkObjectResult(_mapper.Map<IEnumerable<AuthorResponseViewModel>>(authors));
        }
    }
}
