using LibraryWebApp.Application.DTOs.AuthorDTOs;
using LibraryWebApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAuthors()
        {
            var authors = await _authorsService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAuthor(Guid id)
        {
            var author = await _authorsService.GetAuthorByIdAsync(id);
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

            var entity = await _authorsService.AddAuthorAsync(authorDto);
            return CreatedAtAction(nameof(GetAuthor), new { id = entity.Id }, authorDto);
        }

        [HttpPut("updateAuthor/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutAuthor(Guid id, [FromBody] AuthorViewModel authorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _authorsService.UpdateAuthorAsync(id, authorDto);
            return NoContent();
        }

        [HttpDelete("deleteAuthor/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            await _authorsService.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
