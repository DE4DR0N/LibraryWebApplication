using AutoMapper;
using LibraryWebApp.Application.DTOs.UserDTOs;
using LibraryWebApp.Application.Interfaces;
using LibraryWebApp.Domain.Interfaces;

namespace LibraryWebApp.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UsersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<UserViewModel> GetUserByUsernameAsync(string userName)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(userName);
            return _mapper.Map<UserViewModel>(user);
        }
    }
}
