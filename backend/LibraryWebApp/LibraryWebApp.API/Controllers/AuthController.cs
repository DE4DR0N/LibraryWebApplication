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
            if (ModelState.IsValid)
            {
                var result = await _register.ExecuteAsync(model);

                if (result)
                {
                    return Ok(new { Message = "Registration successful" });
                }

                return BadRequest(result);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _login.ExecuteAsync(model);

                if (result.IsLoggedIn)
                {
                    HttpContext.Response.Cookies.Append("tasty-cookies", result.AccessToken, new CookieOptions { HttpOnly = true });
                    return Ok(new { AccessToken = $"{result.AccessToken}" });
                }

                return Unauthorized(new { Message = "Invalid login attempt" });
            }

            return BadRequest(ModelState);
        }
        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenViewModel model)
        {
            var loginResult = await _refreshToken.ExecuteAsync(model);
            if (loginResult.IsLoggedIn) return Ok(loginResult);
            return Unauthorized();
        }
    }
}
