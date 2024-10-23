using LibraryWebApp.Application.DTOs.AuthDTOs;
using LibraryWebApp.Application.Interfaces.SignIn;
using LibraryWebApp.Application.Interfaces.Tokens;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryWebApp.Application.UseCases.SignIn
{
    public class RefreshTokenUseCase : IRefreshTokenUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenerateAccessTokenUseCase _generateAccessTokenUseCase;
        private readonly IGenerateRefreshTokenUseCase _generateRefreshTokenUseCase;
        private readonly IConfiguration _configuration;
        public RefreshTokenUseCase(IUnitOfWork unitOfWork, IConfiguration configuration,
            IGenerateAccessTokenUseCase generateAccessTokenUseCase, IGenerateRefreshTokenUseCase generateRefreshTokenUseCase)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _generateAccessTokenUseCase = generateAccessTokenUseCase;
            _generateRefreshTokenUseCase = generateRefreshTokenUseCase;
        }
        public async Task<IActionResult> ExecuteAsync(RefreshTokenViewModel model)
        {
            var principal = GetTokenPrincipals(model.JwtToken);
            if (principal?.Identity?.Name is null) return new UnauthorizedResult();

            var identityUser = await _unitOfWork.Users.GetByUsernameAsync(principal.Identity.Name);
            if (identityUser is null || identityUser.RefreshToken != model.RefreshToken || identityUser.RefreshTokenExpiry > DateTime.Now)
                return new UnauthorizedResult();
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, identityUser.UserName),
                    new Claim(ClaimTypes.Role, identityUser.Role),
                    new Claim("UserId", identityUser.Id.ToString())
                };

            var accessToken = _generateAccessTokenUseCase.Execute(claims);
            var refreshToken = _generateRefreshTokenUseCase.Execute();
            identityUser.RefreshToken = refreshToken;
            identityUser.RefreshTokenExpiry = DateTime.Now.AddHours(double.Parse(_configuration["Jwt:RefreshTokenExpiration"]));
            _unitOfWork.Users.Update(identityUser);
            await _unitOfWork.CompleteAsync();

            var response = new LoginResponseViewModel(accessToken, refreshToken);
            return new OkObjectResult(new { AccessToken = $"{response.AccessToken}", RefreshToken = $"{response.RefreshToken}" });
        }
        private ClaimsPrincipal? GetTokenPrincipals(string jwtAccess)
        {
            var validation = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
            };

            return new JwtSecurityTokenHandler().ValidateToken(jwtAccess, validation, out _);
        }
    }
}
