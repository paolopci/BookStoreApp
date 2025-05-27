using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Book;
using BookStoreApp.API.Models.Dto;
using BookStoreApp.API.Repositories;
using Microsoft.AspNetCore.Authorization;


namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BooksController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(IBooksRepository booksRepository, IMapper mapper, ILogger<BooksController> logger,
                               IWebHostEnvironment webHostEnvironment)
        {
            _booksRepository = booksRepository;
            _mapper = mapper;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            var books=await _booksRepository.GetAllBooksAsync();
            return Ok(books); ;
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {
            var book = await _booksRepository.GetBookAsync(id);
            return Ok(book);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
        {
            if (id != bookDto.Id)
            {
                return BadRequest();
            }

            // _context.Entry(bookDto).State = EntityState.Modified;

            try
            {
                var book = await _booksRepository.GetAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                if (string.IsNullOrEmpty(bookDto.ImageData)==false)
                {
                    bookDto.Image = CreateFile(bookDto.ImageData, bookDto.OriginalImageName);
                    var picName = Path.GetFileName(book.Image);
                    var path = $"{_webHostEnvironment.WebRootPath}\\bookcoverimages\\{picName}";
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                _mapper.Map(bookDto, book);
                await _booksRepository.UpdateAsync(book);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await BookExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[PutBook] An error occurred while updating book with id {BookId}.", id);
                return StatusCode(500, "An unexpected error occurred.");
            }


        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Book>> PostBook(BookCreateDto bookDto)
        {
           var book = _mapper.Map<Book>(bookDto);
            book.Image= CreateFile(bookDto.ImageData, bookDto.OriginalImageName);
            await _booksRepository.AddAsync(book);
           // await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = _booksRepository.GetAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _booksRepository.DeleteAsync(book.Id);

            return NoContent();
        }

        private async Task<bool> BookExists(int id)
        {
            return await _booksRepository.Exists(id);
        }


        private string CreateFile(string imageBase64, string imageName)
        {
            var url = HttpContext.Request.Host.Value;
            var ext = Path.GetExtension(imageName);
            var filename = $"{Guid.NewGuid().ToString()}{ext}";

            var path = $"{_webHostEnvironment.WebRootPath}\\bookcoverimages\\{filename}";
            byte[] image = Convert.FromBase64String(imageBase64);
            var fileStream = System.IO.File.Create(path);
            fileStream.Write(image, 0, image.Length);
            fileStream.Close();


            return $"https://{url}/bookcoverimages/{filename}";
        }
    }
}
