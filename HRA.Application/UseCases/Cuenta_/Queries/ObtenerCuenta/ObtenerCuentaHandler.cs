using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Registro;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Cuenta_.Queries.ObtenerCuenta
{
    public class ObtenerCuentaHandler : IRequestHandler<CuentaVM, Iresult>
    {
        private readonly IRepository<Cuenta> _repositoryCuenta;
        private readonly IRepository<Persona> _repositoryPersona;
        private readonly IRepository<Documento_persona> _repositoryDocPersona;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
           
        public ObtenerCuentaHandler(
            IRepository<Cuenta> cuentaRepository,
            IRepository<Persona> personaRepository,
            IRepository<Documento_persona> docPersonaRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork, 
            IDateTime dateTime, 
            IHttpContextAccessor httpContextAccessor, 
            IMapper mapper)
        {
            _repositoryCuenta = cuentaRepository;
            _repositoryPersona = personaRepository;
            _repositoryDocPersona= docPersonaRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Iresult> Handle(CuentaVM request, CancellationToken cancellationToken)
        {
            string usuarioModifica = "";

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
            ///  obtener los datos de la cuenta por IdPersona
            /// </summary>

            var personaData = _repositoryPersona.TableNoTracking.FirstOrDefault(x => x.I_ID_PERSONA == request.I_PERSON_ID);
            var documentoData = _repositoryDocPersona.TableNoTracking.FirstOrDefault(x => x.I_ID_PERSONA == request.I_PERSON_ID);
           
            var cuentasAgrupadas = _repositoryCuenta.TableNoTracking
                .Where(x => x.I_ID_PERSONA == request.I_PERSON_ID)
                .GroupBy(cuenta => cuenta.I_ID_PERSONA)
                .Select(s => new CuentaDTO()
                {
                    I_PERSON_ID = request.I_PERSON_ID,
                    V_NRO_DOCUMENT = documentoData!.V_NRO_DOCUMENTO,
                    V_FIRST_NAME = personaData!.V_PRIMER_NOMBRE,
                    V_SECOND_NAME = personaData!.V_SEGUNDO_NOMBRE,
                    V_PATERNAL_LAST_NAME = personaData!.V_APELLIDO_PATERNO,
                    V_MOTHER_LAST_NAME = personaData!.V_APELLIDO_MATERNO,
                    Cuentas_cliente = s.Select(c => new cuenta_cliente
                    {
                        I_ACCOUNT_ID = c.I_ID_CUENTA,
                        I_ACCOUNT_TYPE_ID = c.I_ID_TIPO_CUENTA,
                        V_ACCOUNT_NUMBER = c.V_NUMERO_CUENTA,
                        I_BALANCE = c.I_SALDO,
                        V_USER_CREATE = $"{_repositoryPersona.TableNoTracking.Where(p => p.I_ID_PERSONA == c.I_USUARIO_CREACION).FirstOrDefault()!.V_PRIMER_NOMBRE} " +
                        $"{_repositoryPersona.TableNoTracking.Where(p => p.I_ID_PERSONA == c.I_USUARIO_CREACION).FirstOrDefault()!.V_APELLIDO_PATERNO}",
                        V_USER_MODIF = c.I_USUARIO_MODIFICA == null ? "-" : $"{_repositoryPersona.TableNoTracking.Where(p => p.I_ID_PERSONA == c.I_USUARIO_MODIFICA).FirstOrDefault()!.V_PRIMER_NOMBRE} " +
                        $"{_repositoryPersona.TableNoTracking.Where(p => p.I_ID_PERSONA == c.I_USUARIO_MODIFICA).FirstOrDefault()!.V_APELLIDO_PATERNO}",
                       
                        D_CREATE_DATE = (DateTime)c.D_FECHA_CREACION!,
                        D_MODIF_DATE = c.D_FECHA_MODIFICA,
                        B_STATE = c.B_ESTADO
                    }).OrderBy(c => c.I_ACCOUNT_TYPE_ID).ToList()
                }).ToList();
           
            var map = _mapper.Map<List<CuentaDTO>>(cuentasAgrupadas);

            if (map != null)
            {
                return new SuccessResult<List<CuentaDTO>>(map);
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
