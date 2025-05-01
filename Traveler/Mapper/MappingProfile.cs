using AutoMapper;
using Traveler.Models.Entities;
using Traveler.Models.ViewModels;

namespace Traveler.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>();

            CreateMap<StaysRegViewModel, Stay>()
                .ForMember(dest => dest.StaysPhoto, opt => opt.Ignore());
        }
    }
}
