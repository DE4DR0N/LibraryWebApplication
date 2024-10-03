using AutoMapper;
using LibraryWebApp.Application.DTOs.AuthorDTOs;
using LibraryWebApp.Application.DTOs.BookDTOs;
using LibraryWebApp.Application.DTOs.UserDTOs;
using LibraryWebApp.Domain.Entities;

namespace LibraryWebApp.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookEntity, BookResponseViewModel>().ReverseMap();
            CreateMap<BookEntity, BookViewModel>().ReverseMap();
            CreateMap<AuthorEntity, AuthorResponseViewModel>().ReverseMap();
            CreateMap<AuthorEntity, AuthorViewModel>().ReverseMap();
            CreateMap<UserEntity, UserViewModel>().ReverseMap();
        }
    }
}
