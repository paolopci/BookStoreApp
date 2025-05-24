using BookStoreApp.Blazor.Server.UI.Services.Base;


namespace BookStoreApp.Blazor.Server.UI.Services
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadOnlyDto>>> GetAllAuthors();
        Task<Response<AuthorCreateDto>> AuthorCreateAsync(AuthorCreateDto authorCreateDto);
        Task<Response<AuthorUpdateDto>> AuthorUpdateAsync(AuthorUpdateDto authorUpdateDto);
    }
}