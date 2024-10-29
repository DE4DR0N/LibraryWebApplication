using LibraryWebApp.Application.DTOs.AuthDTOs;
using LibraryWebApp.Application.Interfaces.SignIn;
using LibraryWebApp.Application.Interfaces.SignUp;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterUseCase _register;
        private readonly ILoginUseCase _login;
        private readonly IRefreshTokenUseCase _refreshToken;

        public AuthController(IRegisterUseCase registerUseCase, ILoginUseCase loginUseCase, IRefreshTokenUseCase refreshTokenUseCase)
        {
            _register = registerUseCase;
            _login = loginUseCase;
            _refreshToken = refreshTokenUseCase;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            return await _register.ExecuteAsync(model);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            return await _login.ExecuteAsync(model, HttpContext);
        }
        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenViewModel model)
        {
            return await _refreshToken.ExecuteAsync(model, HttpContext);
        }
    }
}
