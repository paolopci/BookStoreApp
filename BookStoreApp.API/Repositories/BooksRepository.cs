using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Book;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace BookStoreApp.API.Repositories;

public class BooksRepository : GenericRepository<Book>, IBooksRepository
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;


    public BooksRepository(BookStoreDbContext context, IMapper mapper) : base(context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BookReadOnlyDto>> GetAllBooksAsync()
    {
        var books = _mapper.Map<List<BookReadOnlyDto>>(
                             await _context.Books.Include(auth => auth.Author)
                             .ToListAsync()
                             );
        return books;
    }

    public async Task<BookDetailsDto> GetBookAsync(int id)
    {
        var bookEntity = await _context.Books
            .Include(auth => auth.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (bookEntity == null)
        {
            return null;
        }

        var book = _mapper.Map<BookDetailsDto>(bookEntity);

        return book;
    }
}