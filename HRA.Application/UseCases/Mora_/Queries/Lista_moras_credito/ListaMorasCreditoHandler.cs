using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Mora_.Queries.Lista_moras_credito
{
    public class ListaMorasCreditoHandler : IRequestHandler<MorasCreditoVM, Iresult>
    {
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Cuota> _repositoryCuota;
        private readonly IRepository<Mora> _repositoryMora;
        private readonly IRepository<Tipo_mora> _repositoryTipoMora;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListaMorasCreditoHandler(
            IRepository<Usuario> usuarioRepository,
            IRepository<Cuota> cuotaRepository,
            IRepository<Mora> moraRepository,
            IRepository<Tipo_mora> tipoMoraRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositoryUsuario = usuarioRepository;
            _repositoryCuota = cuotaRepository;
            _repositoryMora = moraRepository;
            _repositoryTipoMora = tipoMoraRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(MorasCreditoVM request, CancellationToken cancellationToken)
        {
            var Claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            var claimUserId = Claims.FirstOrDefault(c => c.Type == "IDUser")?.Value;

            var usuario = _repositoryUsuario.TableNoTracking
                .Where(x => x.B_ESTADO == "1" && x.I_ID_USUARIO == Convert.ToInt32(claimUserId)).FirstOrDefault();

            if (usuario is null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 500,
                    Value = new List<DetailError>()
                    {
                        new DetailError("05", "Usuario no autorizado")
                    }
                };
            }

            var moras_credit = _repositoryMora.TableNoTracking.Join(_repositoryCuota.TableNoTracking,
                                M => M.I_ID_CUOTA, C => C.I_ID_CUOTA, (M, C) => new { M, C }).Where(w => w.C.I_ID_CREDITO == request.I_CREDIT_ID && w.C.B_ESTADO == "1")
                                .Join(_repositoryTipoMora.TableNoTracking, MC => MC.M.I_ID_TIPO_MORA, TM => TM.I_ID_TIPO_MORA, (MC, TM) => new { MC, TM })
                                .Select(s => new MorasCreditoDTO
                                {
                                    I_MORA_ID = s.MC.M.I_ID_MORA,
                                    I_CUOTA_ID = s.MC.M.I_ID_CUOTA,
                                    V_TYPE_MORA = s.TM.V_NOMBRE,
                                    I_MORA_AMOUNT = s.MC.M.I_MONTO_MORA,
                                    I_NUMBER_DAYS = s.MC.M.I_NUMERO_DIA,
                                    B_STATE = s.MC.M.B_ESTADO //s.C.B_ESTADO
                                })
                                .ToList();

            var map = _mapper.Map<List<MorasCreditoDTO>>(moras_credit);

            if (map != null)
            {
                return new SuccessResult<List<MorasCreditoDTO>>(map);
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