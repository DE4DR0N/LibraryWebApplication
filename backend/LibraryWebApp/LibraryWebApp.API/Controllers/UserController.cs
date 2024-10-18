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
            return await getBooksByUser.ExecuteAsync(userId);
        }
        [HttpGet("{username}")]
        public async Task<IActionResult> GerUserByUserName(string username)
        {
            return await getUserByUsername.ExecuteAsync(username);
        }
    }
}