using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Providers;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;


namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private const string TokenKey = "authToken";

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, 
                                     AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(LoginUserDto loginModel, CancellationTokenSource token)
        {
            var response = await _httpClient.LoginAsync(loginModel, token.Token);

            if (response != null && !string.IsNullOrEmpty(response.Token))
            {
                // Store the JWT token in local storage
                await _localStorage.SetItemAsync(TokenKey, response.Token);

                // Change auth state of app
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();

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
