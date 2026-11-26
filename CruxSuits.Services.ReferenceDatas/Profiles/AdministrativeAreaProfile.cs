using AutoMapper;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas.AdministrativeArea;

namespace CruxSuits.Services.ReferenceDatas.Profiles;

public sealed class AdministrativeAreaProfile : Profile
{
    public AdministrativeAreaProfile()
    {
        CreateMap<AdministrativeAreaRequest, AdministrativeArea>();
        CreateMap<AdministrativeArea, AdministrativeAreaDetailsResponse>();
    }
}
