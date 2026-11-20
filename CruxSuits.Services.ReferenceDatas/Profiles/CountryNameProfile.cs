using AutoMapper;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas.CountryName;

namespace CruxSuits.Services.ReferenceDatas.Profiles;

public sealed class CountryNameProfile : Profile
{
    public CountryNameProfile()
    {
        CreateMap<CountryNameRequest, CountryName>();
        CreateMap<CountryNameUpdateRequest, CountryName>();

        CreateMap<CountryName, CountryNameResponse>()
            .ForMember(
                destination => destination.LanguageName,
                options => options.MapFrom(source => $"{source.Language!.EnglishName} ({source.Language!.NativeName})"));
    }
}
