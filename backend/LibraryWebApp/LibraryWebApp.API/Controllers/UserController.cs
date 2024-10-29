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

        [HttpGet("{username}/books")]
        public async Task<IActionResult> GetBorrowedBooks([FromRoute] string username)
        {
            return await getBooksByUser.ExecuteAsync(username);
        }
        [HttpGet("{username}")]
        public async Task<IActionResult> GerUserByUserName([FromRoute] string username)
        {
            return await getUserByUsername.ExecuteAsync(username);
        }
    }
}