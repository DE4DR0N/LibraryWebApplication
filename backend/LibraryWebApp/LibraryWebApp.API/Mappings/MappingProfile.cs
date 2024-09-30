using AutoMapper;
using LibraryWebApp.Application.DTOs;
using LibraryWebApp.Domain.Entities;

namespace LibraryWebApp.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookEntity, BookViewModel>().ReverseMap();
            CreateMap<AuthorEntity, AuthorViewModel>().ReverseMap();
            CreateMap<UserEntity, UserViewModel>().ReverseMap();
        }
    }
}
