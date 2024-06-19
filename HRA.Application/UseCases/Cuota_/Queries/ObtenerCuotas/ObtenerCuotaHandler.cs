using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Cuota_.Queries.ObtenerCuotas
{
    public class ObtenerCuotaHandler : IRequestHandler<CuotaVM,Iresult>
    {
        private readonly IRepository<Cuota> _repositoryCuota;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ObtenerCuotaHandler(IRepository<Cuota> cuotaRepository,
        IUnitOfWork unitOfWork, IDateTime dateTime, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _repositoryCuota = cuotaRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(CuotaVM request, CancellationToken cancellationToken)
        {
            var identity = _httpContextAccessor as ClaimContextAccessor;
            //var usuario = _repositoryUsuario.Table.FirstOrDefault(x => x.V_NOMBRE == Convert.ToString(identity.UserName));

            //if (usuario == null)
            //{
            //    return new FailureResult<IEnumerable<DetailError>>()
            //    {
            //        StatusCode = 500,
            //        Value = new List<DetailError>()
            //        {
            //            new DetailError("05", "Usuario no autorizado")
            //        }
            //    };
            //}


            var cuota = _repositoryCuota.TableNoTracking
                .Where(x => x.I_ID_CUOTA == Convert.ToInt32(request.I_INSTALLMENT_ID))
                .ToList();

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
                        new DetailError("01", "No se pudo obtener respuesta")
                    }
                };
            }
        }

    }
}
