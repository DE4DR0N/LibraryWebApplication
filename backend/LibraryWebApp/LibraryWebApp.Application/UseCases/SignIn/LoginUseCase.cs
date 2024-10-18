using LibraryWebApp.Application.DTOs.AuthDTOs;
using LibraryWebApp.Application.Interfaces.SignIn;
using LibraryWebApp.Application.Interfaces.Tokens;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace LibraryWebApp.Application.UseCases.SignIn
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenerateAccessTokenUseCase _generateAccessTokenUseCase;
        private readonly IGenerateRefreshTokenUseCase _generateRefreshTokenUseCase;
        private readonly IConfiguration _configuration;
        public LoginUseCase(IUnitOfWork unitOfWork, IConfiguration configuration,
            IGenerateAccessTokenUseCase generateAccessTokenUseCase, IGenerateRefreshTokenUseCase generateRefreshTokenUseCase)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _generateAccessTokenUseCase = generateAccessTokenUseCase;
            _generateRefreshTokenUseCase = generateRefreshTokenUseCase;
        }
        public async Task<IActionResult> ExecuteAsync(LoginViewModel model, HttpContext httpContext)
        {
            var response = new LoginResponseViewModel();
            var user = await _unitOfWork.Users.GetByUsernameAsync(model.Username);
            if (user != null && BCrypt.Net.BCrypt.EnhancedVerify(model.Password, user.PasswordHash))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim("UserId", user.Id.ToString())
                };

                var accessToken = _generateAccessTokenUseCase.Execute(claims);
                var refreshToken = _generateRefreshTokenUseCase.Execute();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = DateTime.Now.AddHours(double.Parse(_configuration["Jwt:RefreshTokenExpiration"]));
                _unitOfWork.Users.Update(user);
                await _unitOfWork.CompleteAsync();
                response.IsLoggedIn = true;
                response.AccessToken = accessToken;
                response.RefreshToken = refreshToken;
                httpContext.Response.Cookies.Append("tasty-cookies", response.AccessToken, new CookieOptions { HttpOnly = true });
                return new OkObjectResult(new { IsLoggedIn = $"{response.IsLoggedIn}", AccessToken = $"{response.AccessToken}" });
            }
            return new UnauthorizedObjectResult("Invalid login attempt");
        }
    }
}
