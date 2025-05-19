using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;


namespace BookStoreApp.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Example mappings - replace with your actual entities and DTOs
            //  CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Author, AuthorCreateDto>().ReverseMap();

        }
    }
}
