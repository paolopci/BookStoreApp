using Microsoft.AspNetCore.Components.Authorization;


namespace BookStoreApp.Blazor.Server.UI.Providers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ilocal
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
