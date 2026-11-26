using AutoMapper;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeAreaName;

namespace CruxSuits.Services.ReferenceDatas.Profiles;

public sealed class AdministrativeAreaNameProfile : Profile
{
    public AdministrativeAreaNameProfile()
    {
        CreateMap<AdministrativeAreaNameRequest, AdministrativeAreaName>();
        CreateMap<AdministrativeAreaNameUpdateRequest, AdministrativeAreaName>();

        CreateMap<AdministrativeAreaName, AdministrativeAreaNameResponse>()
            .ForMember(
                destination => destination.LanguageName,
                options => options.MapFrom(source => $"{source.Language!.EnglishName} ({source.Language!.NativeName})"));
    }
}
