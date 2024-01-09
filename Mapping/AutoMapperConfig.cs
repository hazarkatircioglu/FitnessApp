using AutoMapper;
using FitnessApp.Models;
using FitnessApp.Models.ViewModels;

namespace FitnessApp.Mapping
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AppUser, UserListVM>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
            CreateMap<AppUser, GetUserVM>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
        }
    }
}
