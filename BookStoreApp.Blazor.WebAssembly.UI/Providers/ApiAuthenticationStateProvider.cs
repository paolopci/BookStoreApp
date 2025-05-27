using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;


namespace BookStoreApp.Blazor.WebAssembly.UI.Providers
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

        /// <summary>
        /// Parses a JWT and returns a JwtSecurityToken instance.
        /// </summary>
        /// <param name="token">The raw JWT string.</param>
        /// <returns>Parsed JwtSecurityToken or null if invalid.</returns>
        public JwtSecurityToken? ParseToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token) || !_jwtSecurityTokenHandler.CanReadToken(token))
            {
                return null;
            }
            return _jwtSecurityTokenHandler.ReadJwtToken(token);
        }


        /// <summary>
        /// Forces a re-evaluation of the authentication state from the current JWT.
        /// </summary>
        public void NotifyAuthenticationStateChanged() =>
            base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());


        public async Task LoggedIn()
        {
            // Recupera il token JWT salvato nello storage locale
            var saveToken = await _localStorage.GetItemAsync<string>("authToken");

            // Decodifica il token JWT per ottenere il contenuto
            var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(saveToken);

            // Estrae i claims dal token decodificato
            var claims = tokenContent.Claims;

            // Crea un oggetto ClaimsPrincipal con i claims estratti e il tipo di autenticazione "apiauth"
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "apiauth"));

            // Crea un nuovo stato di autenticazione per l'utente autenticato
            var authState = Task.FromResult(new AuthenticationState(user));

            // Notifica ai consumer che lo stato di autenticazione è cambiato
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task LoggedOut()
        {
            await _localStorage.RemoveItemAsync("authToken");
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }


    }
}
