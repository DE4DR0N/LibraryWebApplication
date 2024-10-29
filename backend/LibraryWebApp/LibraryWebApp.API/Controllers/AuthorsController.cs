using LibraryWebApp.Application.DTOs.AuthorDTOs;
using LibraryWebApp.Application.Interfaces.Authors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IGetAllAuthorsUseCase _getAllAuthors;
        private readonly IGetAuthorByIdUseCase _getAuthorById;
        private readonly IAddAuthorUseCase _addAuthor;
        private readonly IUpdateAuthorUseCase _updateAuthor;
        private readonly IDeleteAuthorUseCase _deleteAuthor;

        public AuthorsController(IGetAllAuthorsUseCase getAllAuthorsUseCase, IGetAuthorByIdUseCase getAuthorByIdUseCase, 
            IAddAuthorUseCase addAuthorUseCase, IUpdateAuthorUseCase updateAuthorUseCase, IDeleteAuthorUseCase deleteAuthorUseCase)
        {
            _getAllAuthors = getAllAuthorsUseCase;
            _getAuthorById = getAuthorByIdUseCase;
            _addAuthor = addAuthorUseCase;
            _updateAuthor = updateAuthorUseCase;
            _deleteAuthor = deleteAuthorUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            return await _getAllAuthors.ExecuteAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor([FromRoute] Guid id)
        {
            return await _getAuthorById.ExecuteAsync(id);
        }

        [HttpPost("addAuthor")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PostAuthor([FromBody]AuthorViewModel authorDto)
        {
            var author = await _addAuthor.ExecuteAsync(authorDto);
            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("updateAuthor/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutAuthor(Guid id, [FromBody] AuthorViewModel authorDto)
        {
            return await _updateAuthor.ExecuteAsync(id, authorDto);
        }

        [HttpDelete("deleteAuthor/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] Guid id)
        {
            return await _deleteAuthor.ExecuteAsync(id);
        }
    }
}
