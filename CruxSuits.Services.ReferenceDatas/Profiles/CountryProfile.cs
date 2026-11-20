using AutoMapper;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas.Country;

namespace CruxSuits.Services.ReferenceDatas.Profiles
{
    public sealed class CountryProfile : Profile
    {
        public CountryProfile()
        {
            string requestedLanguageId = null!;

            CreateMap<CountryRequest, Country>();

            CreateMap<UpdateCountryRequest, Country>()
                .ForMember(destination => destination.Id, options => options.Ignore());

            CreateMap<Country, CountryResponse>()
                .ForMember(destination => destination.Name, options => options.MapFrom(source =>
                    source.Names
                        .Where(name => name.LanguageId == requestedLanguageId)
                        .Select(name => name.Name)
                        .FirstOrDefault() ?? "Unknown"));

            CreateMap<Country, CountryPhoneCodeResponse>()
                .ForMember(destination => destination.Name, options => options.MapFrom(source =>
                    source.Names
                        .Where(name => name.LanguageId == requestedLanguageId)
                        .Select(name => name.Name)
                        .FirstOrDefault() ?? "Unknown"));

            CreateMap<Country, CountryDetailsResponse>();
        }
    }
}
