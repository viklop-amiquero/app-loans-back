using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Application.UseCases.Cuota_.Queries.ObtenerCuotas;
using HRA.Domain.Entities.RapiDiario;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HRA.Application.UseCases.Cuota_.Queries.Lista_total_cuotas
{
    public class ListaTotalHandler : IRequestHandler<CuotasVM, Iresult>
    {
        private readonly IRepository<Cuota> _repositoryCuota;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListaTotalHandler(IRepository<Cuota> cuotaRepository,
        IUnitOfWork unitOfWork, IDateTime dateTime, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _repositoryCuota = cuotaRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(CuotasVM request, CancellationToken cancellationToken)
        {
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            var cuota = _repositoryCuota.TableNoTracking.ToList();
            var map = _mapper.Map<List<CuotaDTO>>(cuota);

            if (map != null)
            {

                return new SuccessResult<List<CuotaDTO>>(map);

            }
            else
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 500,
                    Value = new List<DetailError>()
                    {
                        new DetailError("01", "No se pudo obtener respuesta.")
                    }
                };
            }
        }

    }
}
