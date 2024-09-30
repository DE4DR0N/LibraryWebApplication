using LibraryWebApp.Application.DTOs;
using LibraryWebApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorViewModel>>> GetAuthors()
        {
            var authors = await _authorsService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AuthorViewModel>> GetAuthor(Guid id)
        {
            var author = await _authorsService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost("AddAuthor")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<AuthorViewModel>> PostAuthor([FromBody]AuthorViewModel authorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _authorsService.AddAuthorAsync(authorDto);
            return CreatedAtAction(nameof(GetAuthor), new { id = authorDto.Id }, authorDto);
        }

        [HttpPut("UpdateAuthor/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> PutAuthor(Guid id, [FromBody] AuthorViewModel authorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _authorsService.UpdateAuthorAsync(authorDto);
            return NoContent();
        }

        [HttpDelete("DeleteAuthor/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            await _authorsService.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
