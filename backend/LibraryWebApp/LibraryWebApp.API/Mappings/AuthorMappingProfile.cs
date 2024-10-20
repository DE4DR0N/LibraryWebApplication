using AutoMapper;
using LibraryWebApp.Application.DTOs.AuthorDTOs;
using LibraryWebApp.Domain.Entities;

namespace LibraryWebApp.API.Mappings
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<AuthorEntity, AuthorResponseViewModel>().ReverseMap();
            CreateMap<AuthorEntity, AuthorViewModel>().ReverseMap();
        }
    }
}
