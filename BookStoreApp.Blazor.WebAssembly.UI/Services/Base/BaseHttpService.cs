using System.Net.Http.Headers;
using Blazored.LocalStorage;


namespace BookStoreApp.Blazor.WebAssembly.UI.Services.Base
{
    public class BaseHttpService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;

        public BaseHttpService(IClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        protected Response<T> ConvertApiException<T>(ApiException apiException)
        {
            var response = new Response<T>();
            if (apiException.StatusCode == 400)
            {
                response.Message = "Validation errors have occurred.";
                response.ValidationErrors = apiException.Response;
                response.Success = false;
            }
            else if (apiException.StatusCode == 404)
            {
                response.Message = "The requested item could not be found.";
                response.Success = false;
            }
            else if (apiException.StatusCode >= 200 && apiException.StatusCode <= 299)
            {
                response.Message = "Request was successful.";
                response.Success = true;
            }
            else
            {
                response.Message = "Something went wrong, please try again.";
                response.Success = false;
            }
            return response;
        }

        protected async Task GetBearerToken()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("bearer", token);
            }
        }
    }

}
