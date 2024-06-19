using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Mora_.Queries.ObtenerMora
{
    public class ObtenerMoraHandler : IRequestHandler<MoraVM, Iresult>
    {
        private readonly IRepository<Mora> _repositoryMora;
        private readonly IRepository<Tipo_mora> _repositoryTipoMora;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObtenerMoraHandler(
            IRepository<Mora> moraRepository,
            IRepository<Tipo_mora> tipoMoraRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositoryMora = moraRepository;
            _repositoryTipoMora = tipoMoraRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Iresult> Handle(MoraVM request, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            var claimUserId = claims?.FirstOrDefault(c => c.Type == "IDUser")?.Value;

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

            int idCuota = Convert.ToInt32(request.V_CUOTA_ID!);

            var mora = _repositoryMora.TableNoTracking
                                .Join(_repositoryTipoMora.TableNoTracking, M => M.I_ID_TIPO_MORA, TM => TM.I_ID_TIPO_MORA, (M, TM) => new { M, TM })
                                .Where(w => w.M.I_ID_CUOTA == idCuota && w.M.B_ESTADO == "1").ToList()
                                .Select(s => new MoraDTO
                                {
                                    I_MORA_ID = s.M.I_ID_MORA,
                                    I_CUOTA_ID = s.M.I_ID_CUOTA,
                                    V_TYPE_MORA = s.TM.V_NOMBRE,
                                    I_MORA_AMOUNT = s.M.I_MONTO_MORA,
                                    I_NUMBER_DAYS = s.M.I_NUMERO_DIA,
                                    B_STATE = s.M.B_ESTADO
                                })
                                .ToList();

            if (mora.Count == 0)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "Mora no encontrada")
                    }
                };
            }

            var map = _mapper.Map<List<MoraDTO>>(mora);

            if (map != null)
            {
                return new SuccessResult<List<MoraDTO>>(map);
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
