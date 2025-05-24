using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;


namespace BookStoreApp.Blazor.Server.UI.Services
{
    public class AuthorService : BaseHttpService, IAuthorService
    {
        private readonly IClient _client;
        public AuthorService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {
            _client = client;
        }


        public async Task<Response<List<AuthorReadOnlyDto>>> GetAllAuthors()
        {
            Response<List<AuthorReadOnlyDto>> response;

            try
            {
                // Recupera e imposta il token Bearer per l'autenticazione delle richieste HTTP.
                await GetBearerToken();

                var data = await _client.AuthorsAllAsync();
                response = new Response<List<AuthorReadOnlyDto>>()
                {
                    Data = data.ToList(),
                    Success = true
                };
            }
            catch (ApiException apiException)
            {
                // Converte l'eccezione API in una risposta standardizzata.
                response = ConvertApiException<List<AuthorReadOnlyDto>>(apiException);
            }

            return response;
        }

        public async Task<Response<AuthorCreateDto>> AuthorCreateAsync(AuthorCreateDto authorCreateDto)
        {
            Response<AuthorCreateDto> response = new() { Success = true };

            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsPOSTAsync(authorCreateDto);
            }
            catch (ApiException apiException)
            {
                // Gestione dell'eccezione API.
                var errorResponse = ConvertApiException<AuthorCreateDto>(apiException);
                // throw new Exception(errorResponse.Message);
            }
            return response;
        }
    }
}
