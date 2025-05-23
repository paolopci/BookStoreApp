using System.Net.Http.Headers;


namespace BookStoreApp.Blazor.Server.UI.Services.Base
{
    public partial class Client : IClient
    {
       // public HttpClient HttpClient { get; }
       public void SetBearerToken(string token)
       {
           _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
