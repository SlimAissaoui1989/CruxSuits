using AutoMapper;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas;

namespace CruxSuits.Services.ReferenceDatas.Profiles
{
    public sealed class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryRequest, Country>();

            CreateMap<UpdateCountryRequest, Country>()
                .ForMember(destination => destination.Id, options => options.Ignore());

            CreateMap<Country, CountryResponse>()
                .ForMember(destination => destination.Name, options => options.MapFrom(source => source.CountryNames.FirstOrDefault()!.Name));

            CreateMap<Country, CountryPhoneCodeResponse>()
                .ForMember(destination => destination.Name, options => options.MapFrom(source => source.CountryNames.FirstOrDefault()!.Name));

            CreateMap<CountryName, CountryResponse>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.CountryId))
                .ForMember(destination => destination.Name, options => options.MapFrom(source => source.Name))
                .ForMember(destination => destination.Alpha3Code, options => options.MapFrom(source => source.Country!.Alpha3Code))
                .ForMember(destination => destination.NumericCode, options => options.MapFrom(source => source.Country!.NumericCode))
                .ForMember(destination => destination.PhoneCode, options => options.MapFrom(source => source.Country!.PhoneCode))
                .ForMember(destination => destination.CurrencyId, options => options.MapFrom(source => source.Country!.CurrencyId));

            CreateMap<CountryName, CountryPhoneCodeResponse>()
                .ForMember(destination => destination.Id, options => options.MapFrom(source => source.CountryId))
                .ForMember(destination => destination.Name, options => options.MapFrom(source => source.Name))
                .ForMember(destination => destination.PhoneCode, options => options.MapFrom(source => source.Country!.PhoneCode));

            CreateMap<Country, CountryDetailsResponse>();
        }
    }
}
