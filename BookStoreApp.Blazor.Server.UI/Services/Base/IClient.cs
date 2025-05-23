namespace BookStoreApp.Blazor.Server.UI.Services.Base
{
    public partial interface IClient
    {
        //public HttpClient HttpClient { get; }

        /// <summary>
        /// Imposta l'header Authorization con il token JWT
        /// </summary>
        void SetBearerToken(string token);
    }
}
