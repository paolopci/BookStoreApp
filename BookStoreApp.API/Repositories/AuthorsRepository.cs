using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using Microsoft.EntityFrameworkCore;


namespace BookStoreApp.API.Repositories;

public class AuthorsRepository : GenericRepository<Author>, IAuthorsRepository
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public AuthorsRepository(BookStoreDbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AuthorDetailsDto> GetAuthorDetailsAsync(int id)
    {
        var author = await _context.Authors
            .Where(a => a.Id == id)
            .ProjectTo<AuthorDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        if (author == null)
        {
            return null;
        }
        return author;
    }
}