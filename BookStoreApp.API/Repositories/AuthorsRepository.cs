using BookStoreApp.API.Data;


namespace BookStoreApp.API.Repositories;

public class AuthorsRepository : GenericRepository<Author>, IAuthorsRepository
{
    public AuthorsRepository(BookStoreDbContext context) : base(context)
    {
    }
}