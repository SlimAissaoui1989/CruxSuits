using AutoMapper;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions;
using CruxSuits.Shared.Permissions.Users;

namespace CruxSuits.BackAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role!.Name))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Person!.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Person!.LastName))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Person!.FullName))
                .ForMember(dest => dest.ProfilePictureSrc, opt => opt.MapFrom(src => src.ProfilePictureId == null ? null : $"api/digitalfile/visualize/{src.ProfilePictureId}"));
        }
    }
}
