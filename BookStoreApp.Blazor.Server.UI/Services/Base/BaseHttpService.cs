using Blazored.LocalStorage;


namespace BookStoreApp.Blazor.Server.UI.Services.Base
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

//        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException apiException)
//        {
//            if (apiException.StatusCode == 400)
//            {
//return Response<Guid>(){Message="Validation errors have occured.",validationErrors=ex.Response}
//            }
//        }
    }
}
