using BookStoreApp.Blazor.Server.UI.Services.Base;


namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient httpClient;

        public AuthenticationService(IClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> AuthenticationAsync(LoginUserDto loginModel, CancellationTokenSource token)
        {
            var response = await httpClient.LoginAsync(loginModel, token.Token);
            return response != null && !string.IsNullOrEmpty(response.Token);
        }
    }
}
