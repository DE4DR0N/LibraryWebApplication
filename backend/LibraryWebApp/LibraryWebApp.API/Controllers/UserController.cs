using LibraryWebApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IBooksService _booksService;
        private readonly IUsersService _usersService;
        public UserController(IBooksService booksService, IUsersService usersService)
        {
            _booksService = booksService;
            _usersService = usersService;
        }

        [HttpGet("{userId}/books")]
        public async Task<IActionResult> GetBorrowedBooks(Guid userId)
        {
            var books = await _booksService.GetBooksByUserAsync(userId);
            if (books == null) return NotFound();
            return Ok(books);
        }
        [HttpGet("{username}")]
        public async Task<IActionResult> GerUserByUserName(string username)
        {
            var user = await _usersService.GetUserByUsernameAsync(username);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}