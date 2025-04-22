using AutoMapper;
using Traveler.DTOs;
using Traveler.Models.Entities;
using Traveler.Models.ViewModels;

namespace Traveler.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, RegisterDto>();
            CreateMap<RegisterViewModel, User>();

            CreateMap<RegisterDto, User>();
        }
    }
}
