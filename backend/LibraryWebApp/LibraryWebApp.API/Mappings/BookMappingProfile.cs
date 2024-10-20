using AutoMapper;
using LibraryWebApp.Application.DTOs.BookDTOs;
using LibraryWebApp.Domain.Entities;

namespace LibraryWebApp.API.Mappings
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<BookEntity, BookResponseViewModel>().ReverseMap();
            CreateMap<BookEntity, BookViewModel>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
