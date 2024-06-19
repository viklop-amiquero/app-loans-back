using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Cancelacion_mora_.Queries.Lista_total_canc_mora
{
    public class ListaCancelacionMoraHandler : IRequestHandler<CancelacionesMoraVM, Iresult>
    {
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Tipo_canc_mora> _repositoryTipoCancMora;
        private readonly IRepository<Cancelacion_mora> _repositoryCancMora;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListaCancelacionMoraHandler(
            IRepository<Usuario> usuarioRepository,
            IRepository<Tipo_canc_mora> tipoCancMoraRepository,
            IRepository<Cancelacion_mora> cancMoraRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositoryUsuario = usuarioRepository;
            _repositoryTipoCancMora = tipoCancMoraRepository;
            _repositoryCancMora = cancMoraRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(CancelacionesMoraVM request, CancellationToken cancellationToken)
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

            //var canc_mora = _repositoryCancMora.TableNoTracking.Where(x => x.I_ID_MORA == request.I_MORA_ID).ToList();

            var canc_mora = _repositoryCancMora.TableNoTracking.Join(_repositoryTipoCancMora.TableNoTracking,
                                CM => CM.I_ID_TIPO_CANC_MORA, TC => TC.I_ID_TIPO_CANC_MORA, (CM, TC) => new { CM, TC }).Where(w => w.CM.I_ID_MORA == request.I_MORA_ID
                                && w.CM.B_ESTADO == "1")
                                .Select(s => new ListaCancelacionMoraDTO
                                {
                                    I_CANC_MORA_ID = s.CM.I_ID_CANC_MORA,
                                    V_TYPE_CANC_MORA = s.TC.V_NOMBRE,
                                    I_AMOUNT_CANC_MORA = s.CM.I_MONTO_CANC_MORA,
                                    I_START_AMOUNT_MORA = s.CM.I_MONTO_INICIAL_MORA,
                                    I_END_AMOUNT_MORA = s.CM.I_MONTO_FINAL_MORA,
                                    B_STATE = s.CM.B_ESTADO,
                                    D_CREATE_DATE = s.CM.D_FECHA_CREACION
                                })
                                .ToList();

            var map = _mapper.Map<List<ListaCancelacionMoraDTO>>(canc_mora);

            if (map != null)
            {
                return new SuccessResult<List<ListaCancelacionMoraDTO>>(map);
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
