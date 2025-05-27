using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepository _authorsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(IAuthorsRepository authorsRepository,IMapper mapper, ILogger<AuthorsController> logger)
        {
            _authorsRepository = authorsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadOnlyDto>>> GetAuthors()
        {
            _logger.LogInformation("[GetAuthors] Starting retrieval of all authors.");
            try
            {
                var authors = _mapper.Map<List<AuthorReadOnlyDto>>(await _authorsRepository.get());
                _logger.LogInformation("[GetAuthors] Retrieved {Count} authors.", authors.Count);
                return Ok(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[GetAuthors] An error occurred while retrieving authors.");
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDetailsDto>> GetAuthor(int id)
        {
            _logger.LogInformation($"{nameof(GetAuthor)} Retrieving author with id {id}.");
            try
            {
                //var author = _mapper.Map<AuthorDetailsDto>(await _context.Authors.FindAsync(id));
                var author = await _authorsRepository.GetAuthorDetailsAsync(id);

                if (author == null)
                {
                    _logger.LogWarning("[GetAuthor] Author with id {AuthorId} not found.", id);
                    return NotFound();
                }

                _logger.LogInformation("[GetAuthor] Successfully retrieved author with id {AuthorId}.", id);
                return Ok(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[GetAuthor] An error occurred while retrieving author with id {AuthorId}.", id);
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
            _logger.LogInformation("[PutAuthor] Updating author with id {AuthorId}.", id);

            if (id != authorDto.Id)
            {
                _logger.LogWarning("[PutAuthor] Mismatch between route id {RouteId} and body id {BodyId}.", id,
                    authorDto.Id);
                return BadRequest("ID nel percorso diverso dall'ID nel body.");
            }

            try
            {
                var author = await _authorsRepository.GetAsync(id);
                if (author == null)
                {
                    _logger.LogWarning("[PutAuthor] Author with id {AuthorId} not found.", id);
                    return NotFound();
                }

                //questa chiamata riempie o sovrascrive i valori dell’istanza author esistente,
                //copiando i dati da authorDto.
                _authorsRepository.UpdateAsync(author);
                _mapper.Map(authorDto, author);
               
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "[PutAuthor] Concurrency error while updating author with id {AuthorId}.", id);
                if (!await AuthorExists(id))
                {
                    _logger.LogWarning("[PutAuthor] Author with id {AuthorId} no longer exists.", id);
                    return NotFound();
                }

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[PutAuthor] An error occurred while updating author with id {AuthorId}.", id);
                return StatusCode(500, "An unexpected error occurred.");
            }


        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorDto)
        {
            _logger.LogInformation("[PostAuthor] Creating a new author.");
            try
            {
                // questa chiamata crea semplicemente una nuova istanza di Author
                // popolata con i valori presenti in authorDto
                var author = _mapper.Map<Author>(authorDto);

                await _authorsRepository.AddAsync(author);

                _logger.LogInformation("[PostAuthor] Successfully created author with id {AuthorId}.", author.Id);
                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[PostAuthor] An error occurred while creating a new author.");
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            _logger.LogInformation("[DeleteAuthor] Deleting author with id {AuthorId}.", id);
            try
            {
                var author = await _authorsRepository.GetAsync(id);
                if (author == null)
                {
                    _logger.LogWarning("[DeleteAuthor] Author with id {AuthorId} not found.", id);
                    return NotFound();
                }

                _authorsRepository.DeleteAsync(author.Id);

                _logger.LogInformation("[DeleteAuthor] Successfully deleted author with id {AuthorId}.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[DeleteAuthor] An error occurred while deleting author with id {AuthorId}.", id);
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _authorsRepository.Exists(id);
        }

    }
}
