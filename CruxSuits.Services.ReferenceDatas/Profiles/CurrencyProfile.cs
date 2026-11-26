using AutoMapper;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas.Currency;

namespace CruxSuits.Services.ReferenceDatas.Profiles;

public sealed class CurrencyProfile : Profile
{
    public CurrencyProfile()
    {
        string requestedLanguageId = null!;

        CreateMap<CurrencyRequest, Currency>();
        CreateMap<UpdateCurrencyRequest, Currency>()
            .ForMember(destination => destination.Id, options => options.Ignore());

        CreateMap<Currency, CurrencyResponse>()
            .ForMember(destination => destination.Name, options => options.MapFrom(source =>
                source.Names
                    .Where(name => name.LanguageId == requestedLanguageId)
                    .Select(name => name.Name)
                    .FirstOrDefault() ?? "Unknown"));

        CreateMap<Currency, CurrencyDetailsResponse>();
    }
}
