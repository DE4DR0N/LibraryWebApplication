using LibraryWebApp.Application.DTOs.AuthDTOs;
using LibraryWebApp.Application.Interfaces;
using LibraryWebApp.Domain.Entities;
using LibraryWebApp.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryWebApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _configuration = configuration;
        }
        public async Task<LoginResponseViewModel> LoginAsync(LoginViewModel model)
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

                var accessToken = _tokenService.GenerateAccessToken(claims);
                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = DateTime.Now.AddHours(double.Parse(_configuration["Jwt:RefreshTokenExpiration"]));
                await _unitOfWork.Users.UpdateAsync(user);
                await _unitOfWork.CompleteAsync();
                response.IsLoggedIn = true;
                response.AccessToken = accessToken;
                response.RefreshToken = refreshToken;
                return response;
            }
            return response;
        }
        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(model.Username);
            if (user != null)
            {
                return false;
            }
            var newUser = new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = model.Username,
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(model.Password),
                Role = "User"
            };

            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<LoginResponseViewModel> RefreshTokenAsync(RefreshTokenViewModel model)
        {
            var principal = GetTokenPrincipals(model.JwtToken);
            var response = new LoginResponseViewModel();
            if (principal?.Identity?.Name is null) return response;

            var identityUser = await _unitOfWork.Users.GetByUsernameAsync(principal.Identity.Name);
            if (identityUser is null || identityUser.RefreshToken != model.RefreshToken || identityUser.RefreshTokenExpiry > DateTime.Now) 
                return response;
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, identityUser.UserName),
                    new Claim(ClaimTypes.Role, identityUser.Role),
                    new Claim("UserId", identityUser.Id.ToString())
                };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            identityUser.RefreshToken = refreshToken;
            identityUser.RefreshTokenExpiry = DateTime.Now.AddHours(double.Parse(_configuration["Jwt:RefreshTokenExpiration"]));
            await _unitOfWork.Users.UpdateAsync(identityUser);
            await _unitOfWork.CompleteAsync();

            response.IsLoggedIn = true;
            response.AccessToken = accessToken;
            response.RefreshToken = refreshToken;
            return response;
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
