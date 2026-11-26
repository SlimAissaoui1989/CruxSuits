using AutoMapper;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas.Language;

namespace CruxSuits.Services.ReferenceDatas.Profiles;

public sealed class LanguageProfile : Profile
{
    public LanguageProfile()
    {
        CreateMap<Language, LanguageResponse>();
        CreateMap<Language, LanguageDetailsResponse>();
        CreateMap<LanguageRequest, Language>();
        CreateMap<UpdateLanguageRequest, Language>();
    }
}
