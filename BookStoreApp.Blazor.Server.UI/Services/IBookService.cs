using BookStoreApp.Blazor.Server.UI.Services.Base;


namespace BookStoreApp.Blazor.Server.UI.Services;

public interface IBookService
{
    Task<Response<BookReadOnlyDto>> GetBookByIdAsync(int id);
    Task<Response<List<BookReadOnlyDto>>> GetAllBooks();
    Task<Response<BookCreateDto>> BookCreateAsync(BookCreateDto bookCreateDto);
    Task<Response<BookUpdateDto>> BookUpdateAsync(BookUpdateDto bookUpdateDto);
    Task<Response<BookDetailsDto>> GetBookDetailsAsync(int id);
    Task<Response<int>> BookDeleteAsync(int id);
}