using AutoMapper;
using ReviewApp.Dto;
using ReviewApp.Models;

namespace ReviewApp.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap <Game, GameDto>();
            CreateMap <Genre, GenreDto>();
            CreateMap <Platform, PlatformDto>();
            CreateMap <Publisher, PublisherDto>();
            CreateMap <User, UserDto>();
            CreateMap <User, UserUpdateDto>();
            CreateMap <Review, ReviewDto>();
            CreateMap <GameUser, GameUserCreateDto>();
            CreateMap <Game, GameDto>().ReverseMap();
            CreateMap <Game, GameUpdateDto>().ReverseMap();
            CreateMap <Genre, GenreDto>().ReverseMap();
            CreateMap <Platform, PlatformDto>().ReverseMap();
            CreateMap <Publisher, PublisherDto>().ReverseMap();
            CreateMap <User, GenreDto>().ReverseMap();
            CreateMap <User, UserUpdateDto>().ReverseMap();
            CreateMap <Review, ReviewDto>().ReverseMap();
            CreateMap <GameUser, GameUserCreateDto>().ReverseMap();
        }
    }
}
