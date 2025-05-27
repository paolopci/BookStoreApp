using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services;

public interface IBookService
{
    Task<Response<BookReadOnlyDto>> GetBookByIdAsync(int id);
    Task<Response<List<BookReadOnlyDto>>> GetAllBooks();
    Task<Response<BookCreateDto>> BookCreateAsync(BookCreateDto bookCreateDto);
    Task<Response<BookUpdateDto>> BookUpdateAsync(int id);
    Task<Response<BookDetailsDto>> GetBookDetailsAsync(int id);
    Task<Response<int>> GetBookEdit(int id,BookUpdateDto book);

    Task<Response<int>> BookDeleteAsync(int id);
}