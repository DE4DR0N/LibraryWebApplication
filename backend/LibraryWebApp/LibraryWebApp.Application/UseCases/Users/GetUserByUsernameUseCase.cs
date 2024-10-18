using AutoMapper;
using LibraryWebApp.Application.DTOs.UserDTOs;
using LibraryWebApp.Application.Interfaces.Users;
using LibraryWebApp.Domain.Interfaces;

namespace LibraryWebApp.Application.UseCases.Users
{
    public class GetUserByUsernameUseCase : IGetUserByUsernameUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUserByUsernameUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserViewModel> ExecuteAsync(string userName)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(userName);
            return _mapper.Map<UserViewModel>(user);
        }
    }
}
