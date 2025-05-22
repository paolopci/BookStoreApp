using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;


namespace BookStoreApp.Blazor.Server.UI.Providers
{
    public class ApiAuthenticationStateProvider:AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var saveToken = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(saveToken))
            {
                // No token: user is not authenticated, anonymous
                var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                return new AuthenticationState(anonymous);
            }

            try
            {
                // Parse and validate the JWT token
                var jwtToken = _jwtSecurityTokenHandler.ReadJwtToken(saveToken);

                // Optional: Check if the token is expired
                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                   // Token expired: remove and return anonymous
                   await _localStorage.RemoveItemAsync("authToken");
                    var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
                    return new AuthenticationState(anonymous);
                }

                // Create identity from token claims
                var claims = jwtToken.Claims;
                var identity = new ClaimsIdentity(claims, "apiauth");
                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


    }
}
