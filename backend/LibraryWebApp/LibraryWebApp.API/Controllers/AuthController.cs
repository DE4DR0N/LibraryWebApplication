using LibraryWebApp.Application.DTOs;
using LibraryWebApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.RegisterAsync(model);

                if (result)
                {
                    return Ok(new { Message = "Registration successful" });
                }

                return BadRequest(result);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.LoginAsync(model);

                if (result.IsLoggedIn)
                {
                    return Ok(new { Token = result });
                }

                return Unauthorized(new { Message = "Invalid login attempt" });
            }

            return BadRequest(ModelState);
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenViewModel model)
        {
            var loginResult = await _authService.RefreshTokenAsync(model);
            if (loginResult.IsLoggedIn) return Ok(loginResult);
            return Unauthorized();
        }
    }
}
