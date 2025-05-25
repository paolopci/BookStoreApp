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


        public async Task<Response<AuthorReadOnlyDto>> GetAuthorByIdAsync(int id)
        {
            Response<AuthorReadOnlyDto> response;
            try
            {
                await GetBearerToken();
                var data = await _client.AuthorsGETAsync(id);

                // Map AuthorDetailsDto to AuthorReadOnlyDto
                var authorReadOnlyDto = new AuthorReadOnlyDto
                {
                    Id = data.Id,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Bio = data.Bio
                };

                response = new Response<AuthorReadOnlyDto> { Data = authorReadOnlyDto, Success = true };
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<AuthorReadOnlyDto>(ex);
            }
            return response;
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

        public async Task<Response<AuthorUpdateDto>> AuthorUpdateAsync(AuthorUpdateDto authorUpdateDto)
        {
            Response<AuthorUpdateDto> response = new() { Success = true };
            try
            {
                await GetBearerToken();
                await _client.AuthorsPUTAsync(authorUpdateDto.Id, authorUpdateDto);
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<AuthorUpdateDto>(ex);
            }

            return response;
        }

        public async Task<Response<AuthorDetailsDto>> GetAuthorDetailsAsync(int id)
        {
            Response<AuthorDetailsDto> response = new() { Success = true };
            try
            {

                await GetBearerToken();
                var data = await _client.AuthorsGETAsync(id);
                response.Data = data;
                response.Success = true;
            }
            catch (ApiException ex)
            {
                response = ConvertApiException<AuthorDetailsDto>(ex);

            }
            return response;
        }

        public async Task<Response<int>>  AuthorDeleteAsync(int id)
        {
            Response<int> response = new();
            try
            {
                await GetBearerToken();
                await _client.AuthorsDELETEAsync(id);
                
            }
            
            catch (ApiException ex)
            {
                
                response=ConvertApiException<int>(ex);
            }
            return response;
        }
    }
}
