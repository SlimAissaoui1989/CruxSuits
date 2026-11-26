using AutoMapper;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas.LocalityName;

namespace CruxSuits.Services.ReferenceDatas.Profiles;

public sealed class LocalityNameProfile : Profile
{
    public LocalityNameProfile()
    {
        CreateMap<LocalityNameRequest, LocalityName>();
        CreateMap<LocalityNameUpdateRequest, LocalityName>();

        CreateMap<LocalityName, LocalityNameResponse>()
            .ForMember(
                destination => destination.LanguageName,
                options => options.MapFrom(source => $"{source.Language!.EnglishName} ({source.Language!.NativeName})"));
    }
}
