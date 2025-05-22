using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;


namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private const string TokenKey = "authToken";

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<bool> AuthenticationAsync(LoginUserDto loginModel, CancellationTokenSource token)
        {
            var response = await _httpClient.LoginAsync(loginModel, token.Token);

            if (response != null && !string.IsNullOrEmpty(response.Token))
            {
                // Store the JWT token in local storage
                await _localStorage.SetItemAsync(TokenKey, response.Token);
                return true;
            }
            return false;
        }




        public async Task LogOut()
        {
            // Remove the JWT token from local storage
            await _localStorage.RemoveItemAsync(TokenKey);
        }
    }
}
