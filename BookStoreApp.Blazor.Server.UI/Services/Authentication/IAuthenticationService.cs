using BookStoreApp.Blazor.Server.UI.Services.Base;


namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticationAsync(LoginUserDto loginModel,CancellationTokenSource token);
        Task LogOut();
    }
}
