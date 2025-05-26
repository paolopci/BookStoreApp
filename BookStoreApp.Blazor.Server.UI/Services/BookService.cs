using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;


namespace BookStoreApp.Blazor.Server.UI.Services;

public class BookService : BaseHttpService, IBookService
{
    private readonly IClient _client;


    public BookService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
    {
        _client = client;
    }


    public async Task<Response<BookReadOnlyDto>> GetBookByIdAsync(int id)
    {
        Response<BookReadOnlyDto> response;
        try
        {
            await GetBearerToken();
            var data = await _client.BooksGETAsync(id);

            // Map AuthorDetailsDto to AuthorReadOnlyDto
            var bookReadOnlyDto = new BookReadOnlyDto()
            {
                Id = data.Id,
                Title = data.Title,
                Image = data.Image,
                Price = data.Price,
                AuthorId = data.AuthorId,
                AuthorName = data.AuthorName
            };

            response = new Response<BookReadOnlyDto> { Data = bookReadOnlyDto, Success = true };
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<BookReadOnlyDto>(ex);
        }
        return response;
    }

    public async Task<Response<List<BookReadOnlyDto>>> GetAllBooks()
    {
        Response<List<BookReadOnlyDto>> response;

        try
        {
            // Recupera e imposta il token Bearer per l'autenticazione delle richieste HTTP.
            await GetBearerToken();

            var data = await _client.BooksAllAsync();
            response = new Response<List<BookReadOnlyDto>>()
            {
                Data = data.ToList(),
                Success = true
            };
        }
        catch (ApiException apiException)
        {
            // Converte l'eccezione API in una risposta standardizzata.
            response = ConvertApiException<List<BookReadOnlyDto>>(apiException);
        }

        return response;
    }

    public async Task<Response<BookCreateDto>> BookCreateAsync(BookCreateDto bookCreateDto)
    {
        Response<BookCreateDto> response = new() { Success = true };

        try
        {
            await GetBearerToken();
            var data = await _client.BooksPOSTAsync(bookCreateDto);
        }
        catch (ApiException apiException)
        {
            // Gestione dell'eccezione API.
            var errorResponse = ConvertApiException<BookCreateDto>(apiException);
            // throw new Exception(errorResponse.Message);
        }
        return response;
    }

    public async Task<Response<BookUpdateDto>> BookUpdateAsync(BookUpdateDto bookUpdateDto)
    {
        Response<BookUpdateDto> response = new() { Success = true };
        try
        {
            await GetBearerToken();
            await _client.BooksPUTAsync(bookUpdateDto.Id, bookUpdateDto);
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<BookUpdateDto>(ex);
        }

        return response;
    }

    public async Task<Response<BookDetailsDto>> GetBookDetailsAsync(int id)
    {
        Response<BookDetailsDto> response = new() { Success = true };
        try
        {

            await GetBearerToken();
            var data = await _client.BooksGETAsync(id);
            response.Data = data;
            response.Success = true;
        }
        catch (ApiException ex)
        {
            response = ConvertApiException<BookDetailsDto>(ex);

        }
        return response;
    }

    public async Task<Response<int>> BookDeleteAsync(int id)
    {
        Response<int> response = new();
        try
        {
            await GetBearerToken();
            await _client.BooksDELETEAsync(id);

        }

        catch (ApiException ex)
        {

            response = ConvertApiException<int>(ex);
        }
        return response;
    }
}