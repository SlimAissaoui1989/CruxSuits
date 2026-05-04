using AutoMapper;
using CruxSuits.Infrastructure.EntityFramework.Entities.Permissions.UIResources;
using CruxSuits.Shared.Permissions.UiResources;

namespace CruxSuits.BackAPI.Profiles
{
    public class UiResourcePermissionProfile : Profile
    {
        public UiResourcePermissionProfile()
        {
            CreateMap<UiResourcePermissionBase, UiResourcePermissionResponse>()
                .ForMember(dest => dest.IsInheritedFromRole, opt => opt.MapFrom(src => src is RoleUiResourcePermission));

            CreateMap<UiResource, UiResourceResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Names!.FirstOrDefault()!.Name))
                .ForMember(dest => dest.UiResourceTypeName, opt => opt.MapFrom(src => src.UiResourceType!.Names!.FirstOrDefault()!.Name));
        }
    }
}
