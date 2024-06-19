using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Tipo_canc_mora_.Queries.Obtener_tipo_canc_mora
{
    public class ObtenerTipoCancMoraHandler : IRequestHandler<TipoCancMoraVM, Iresult>
    {
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Tipo_canc_mora> _repositoryTipoCancMora;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObtenerTipoCancMoraHandler(
            IRepository<Usuario> usuarioRepository,
            IRepository<Tipo_canc_mora> tipoCancMoraRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositoryUsuario = usuarioRepository;
            _repositoryTipoCancMora = tipoCancMoraRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(TipoCancMoraVM request, CancellationToken cancellationToken)
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

            var tipo_canc_mora = _repositoryTipoCancMora.TableNoTracking.Where(x => x.I_ID_TIPO_CANC_MORA.ToString() == request.I_TYPE_CANC_MORA_ID).ToList();

            var map = _mapper.Map<List<TipoCancMoraDTO>>(tipo_canc_mora);

            if (map != null)
            {
                return new SuccessResult<List<TipoCancMoraDTO>>(map);
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
