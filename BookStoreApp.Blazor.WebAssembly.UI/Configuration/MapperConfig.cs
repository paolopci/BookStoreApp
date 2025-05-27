using AutoMapper;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;


namespace BookStoreApp.Blazor.WebAssembly.UI.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Add your mapping configurations here

            CreateMap<AuthorDetailsDto, AuthorUpdateDto>().ReverseMap();
            CreateMap<BookDetailsDto, BookUpdateDto>().ReverseMap();
        }
    }
}
