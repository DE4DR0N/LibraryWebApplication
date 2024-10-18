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
        public async Task<ActionResult> GetAuthors()
        {
            var authors = await _getAllAuthors.ExecuteAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAuthor(Guid id)
        {
            var author = await _getAuthorById.ExecuteAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost("addAuthor")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> PostAuthor([FromBody]AuthorViewModel authorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var entity = await _addAuthor.ExecuteAsync(authorDto);
            return CreatedAtAction(nameof(GetAuthor), new { id = entity.Id }, authorDto);
        }

        [HttpPut("updateAuthor/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutAuthor(Guid id, [FromBody] AuthorViewModel authorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _updateAuthor.ExecuteAsync(id, authorDto);
            return NoContent();
        }

        [HttpDelete("deleteAuthor/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            await _deleteAuthor.ExecuteAsync(id);
            return NoContent();
        }
    }
}
