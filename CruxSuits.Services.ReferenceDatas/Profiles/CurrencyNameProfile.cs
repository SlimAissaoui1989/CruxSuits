using AutoMapper;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas.CurrencyName;

namespace CruxSuits.Services.ReferenceDatas.Profiles;

public sealed class CurrencyNameProfile : Profile
{
    public CurrencyNameProfile()
    {
        CreateMap<CurrencyNameRequest, CurrencyName>();
        CreateMap<CurrencyNameUpdateRequest, CurrencyName>();

        CreateMap<CurrencyName, CurrencyNameResponse>()
            .ForMember(
                destination => destination.LanguageName,
                options => options.MapFrom(source => $"{source.Language!.EnglishName} ({source.Language!.NativeName})"));
    }
}
