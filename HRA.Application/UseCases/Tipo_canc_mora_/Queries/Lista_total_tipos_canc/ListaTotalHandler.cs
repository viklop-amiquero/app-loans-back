using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Application.UseCases.Tipo_canc_mora_.Queries.Obtener_tipo_canc_mora;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Tipo_canc_mora_.Queries.Lista_total_tipos_canc
{
    public class ListaTotalHandler : IRequestHandler<TiposCancMoraVM, Iresult>
    {
        private readonly IRepository<Tipo_canc_mora> _repositoryTipoCancMora;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListaTotalHandler(
            IRepository<Tipo_canc_mora> tipoCancMoraRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositoryTipoCancMora = tipoCancMoraRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(TiposCancMoraVM request, CancellationToken cancellationToken)
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

            var tipos_canc_mora = _repositoryTipoCancMora.TableNoTracking.Where(x => x.B_ESTADO == "1").ToList();
            var map = _mapper.Map<List<TipoCancMoraDTO>>(tipos_canc_mora);

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
