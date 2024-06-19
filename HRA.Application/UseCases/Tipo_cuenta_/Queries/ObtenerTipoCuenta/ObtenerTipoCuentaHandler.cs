using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Tipo_cuenta_.Queries.ObtenerTipoCuenta
{
    public class ObtenerTipoCuentaHandler : IRequestHandler<TipoCuentaVM, Iresult>
    {
        private readonly IRepository<Cuenta> _repositoryCuenta;
        private readonly IRepository<Tipo_cuenta> _repositoryTipoCuenta;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ObtenerTipoCuentaHandler(
            IRepository<Cuenta> cuentaRepository,
            IRepository<Tipo_cuenta> tipoCuentaRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositoryCuenta = cuentaRepository;
            _repositoryTipoCuenta = tipoCuentaRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Iresult> Handle(TipoCuentaVM request, CancellationToken cancellationToken)
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

            /// <summary>
            ///  obtener los datos de tipo cuenta por IdPersona 
            /// </summary>


            var cuentaData = _repositoryTipoCuenta.TableNoTracking.ToList()
                .Join(_repositoryCuenta.TableNoTracking.ToList(), TC => TC.I_ID_TIPO_CUENTA, C => C.I_ID_TIPO_CUENTA, (TC, C) => new { TC, C }).Where(x => x.C.I_ID_PERSONA == request.I_PERSON_ID)
                .Select(s => new TipoCuentaDTO
                {
                    I_PERSON_ID = s.C.I_ID_PERSONA,
                    V_NUMBER_ACCOUNT = s.C.V_NUMERO_CUENTA,
                    I_ACCOUNT_TYPE_ID = s.TC.I_ID_TIPO_CUENTA,
                    V_TYPE_ACCOUNT = s.TC.V_TIPO_CUENTA
                }).ToList();
            if (cuentaData != null)
            {
                return new SuccessResult<List<TipoCuentaDTO>>(cuentaData);
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
