using Blazored.LocalStorage;
using BookStoreApp.Blazor.WebAssembly.UI;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<ApiAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(p =>
                                                           p.GetRequiredService<ApiAuthenticationStateProvider>());

builder.Services.AddHttpClient<IClient, Client>("Default", client =>
{
    client.BaseAddress = new Uri("https://localhost:7073/");
});
builder.Services.AddAutoMapper(typeof(MapperConfig));




await builder.Build().RunAsync();
