using AutoMapper;
using CruxSuits.Services.ReferenceDatas.Infrastructure.EntityFramework.Entities;
using CruxSuits.Shared.DTOs.ReferenceDatas.Locality;

namespace CruxSuits.Services.ReferenceDatas.Profiles;

public sealed class LocalityProfile : Profile
{
    public LocalityProfile()
    {
        CreateMap<LocalityRequest, Locality>();
        CreateMap<Locality, LocalityDetailsResponse>();
    }
}
