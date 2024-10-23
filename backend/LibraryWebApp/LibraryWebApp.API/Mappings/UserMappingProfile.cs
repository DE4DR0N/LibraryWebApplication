using AutoMapper;
using LibraryWebApp.Application.DTOs.UserDTOs;
using LibraryWebApp.Domain.Entities;

namespace LibraryWebApp.API.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserEntity, UserResponseViewModel>().ReverseMap();
        }
    }
}
