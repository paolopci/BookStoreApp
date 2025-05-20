using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Models.Book;
using BookStoreApp.API.Models.Dto;
using BookStoreApp.API.Models.User;


namespace BookStoreApp.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Example mappings - replace with your actual entities and DTOs
            CreateMap<Author, AuthorCreateDto>().ReverseMap();
            CreateMap<Author, AuthorReadOnlyDto>().ReverseMap();
            CreateMap<Author, AuthorUpdateDto>().ReverseMap();




            // BookCreateDto -> Book
            CreateMap<BookCreateDto, Book>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year));


            // BookUpdateDto -> Book
            CreateMap<BookUpdateDto, Book>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year));

            // Book -> BookUpdateDto
            CreateMap<Book, BookUpdateDto>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId ?? 0))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price ?? 0))
                .ForMember(dest => dest.Isbn, opt => opt.MapFrom(src => src.Isbn))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year ?? 0));





            // Book -> BookReadOnlyDto
            CreateMap<Book, BookReadOnlyDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author != null ? $"{src.Author.FirstName} {src.Author.LastName}" : string.Empty))
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId ?? 0))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price ?? 0));


            CreateMap<Book, BookDetailsDto>().ForMember(dest => dest.AuthorName, opt =>
                       opt.MapFrom(src => src.Author != null ? $"{src.Author.FirstName} {src.Author.LastName}" : string.Empty));
            CreateMap<BookDetailsDto, Book>();


            // CreateMap<ApiUser, UserDto>().ReverseMap();
            // DTO -> Entity
            CreateMap<UserDto, ApiUser>()
                .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email,
                           opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName,
                           opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName,
                           opt => opt.MapFrom(src => src.LastName));
            // (eventualmente rimuovi o rimappa anche Role, se non esiste in ApiUser)

            // Entity -> DTO
            CreateMap<ApiUser, UserDto>()
                .ForMember(dest => dest.Email,
                           opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName,
                           opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName,
                           opt => opt.MapFrom(src => src.LastName));
        }
    }
}
