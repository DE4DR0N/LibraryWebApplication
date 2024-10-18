using AutoMapper;
using LibraryWebApp.Application.Interfaces.Authors;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.UseCases.Authors
{
    public class DeleteAuthorUseCase : IDeleteAuthorUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAuthorUseCase(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> ExecuteAsync(Guid id)
        {
            var author = await _unitOfWork.Authors.GetByIdAsync(id);
            if (author == null) return new NotFoundObjectResult("Author not found");
            _unitOfWork.Authors.Delete(author);
            await _unitOfWork.CompleteAsync();
            return new NoContentResult();
        }
    }
}
