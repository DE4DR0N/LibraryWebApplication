using LibraryWebApp.Application.Interfaces.Books;
using LibraryWebApp.Application.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IGetBooksByUserUseCase getBooksByUser;
        private readonly IGetUserByUsernameUseCase getUserByUsername;
        public UserController(IGetBooksByUserUseCase getBooksByUserUseCase, IGetUserByUsernameUseCase getUserByUsernameUseCase)
        {
            getUserByUsername = getUserByUsernameUseCase;
            getBooksByUser = getBooksByUserUseCase;
        }

        [HttpGet("{userId}/books")]
        public async Task<IActionResult> GetBorrowedBooks(Guid userId)
        {
            var books = await getBooksByUser.ExecuteAsync(userId);
            if (books == null) return NotFound();
            return Ok(books);
        }
        [HttpGet("{username}")]
        public async Task<IActionResult> GerUserByUserName(string username)
        {
            var user = await getUserByUsername.ExecuteAsync(username);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}