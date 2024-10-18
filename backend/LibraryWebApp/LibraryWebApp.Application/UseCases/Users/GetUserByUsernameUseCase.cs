using AutoMapper;
using LibraryWebApp.Application.DTOs.UserDTOs;
using LibraryWebApp.Application.Interfaces.Users;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> ExecuteAsync(string userName)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(userName);
            if (user == null) return new NotFoundObjectResult("User not found");
            return new OkObjectResult(_mapper.Map<UserViewModel>(user));
        }
    }
}
