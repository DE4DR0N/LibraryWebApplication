using LibraryWebApp.Application.DTOs.AuthDTOs;
using LibraryWebApp.Application.Interfaces.SignUp;
using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Application.UseCases.SignUp
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> ExecuteAsync(RegisterViewModel model)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(model.Username);
            if (user != null) throw new Exception("Conflict User");
            var newUser = new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = model.Username,
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password),
                Role = "User"
            };

            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.CompleteAsync();
            return new OkObjectResult("Registration successful");
        }
    }
}
