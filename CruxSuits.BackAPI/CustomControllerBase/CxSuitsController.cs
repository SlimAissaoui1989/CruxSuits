using AutoMapper;
using CruxIT.Library.API.Controller;
using CruxSuits.Infrastructure.EntityFramework;
using CruxSuits.Infrastructure.RepositoryPatterns;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CruxSuits.BackAPI.CustomControllerBase
{
    public class CxSuitsController : CxControllerBase<CruxSuitsContext, ICruxSuitsUnitOfWork>
    {
        public CxSuitsController(IConfiguration configuration,
            ILogger<CxControllerBase<CruxSuitsContext, ICruxSuitsUnitOfWork>> logger,
            ICruxSuitsUnitOfWork unitOfWork
            , IMapper mapper) 
            : base(configuration, logger, unitOfWork, mapper)
        {
        }
    }
}
