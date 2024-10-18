using AutoMapper;
using LibraryWebApp.Application.Interfaces.Authors;
using LibraryWebApp.Domain.Interfaces;

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
        public async Task ExecuteAsync(Guid id)
        {
            await _unitOfWork.Authors.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
