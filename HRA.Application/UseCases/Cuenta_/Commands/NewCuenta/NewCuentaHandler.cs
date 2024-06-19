using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Registro;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Cuenta_.Commands.NewCuenta
{
    public class NewCuentaHandler : IRequestHandler<NewCuentaVM, Iresult>
    {
        private readonly IRepository<Cuenta> _repositoryCuenta;
        private readonly IRepository<Persona> _repositoryPersona;
        private readonly IRepository<Documento_persona> _repositoryDocPersona;
        private readonly IRepository<Puesto_persona> _repositoryPuestoPers;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public NewCuentaHandler(
            IRepository<Cuenta> cuentaRepository,
            IRepository<Persona> personaRepository,
            IRepository<Documento_persona> docPersonaRepository,
            IRepository<Puesto_persona> puestoPersRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccesor)
        {
            _repositoryCuenta = cuentaRepository;
            _repositoryPersona = personaRepository;
            _repositoryDocPersona = docPersonaRepository;
            _repositoryPuestoPers = puestoPersRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(NewCuentaVM request, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccesor?.HttpContext?.User?.Claims;
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
            var persona = _repositoryPersona.Table.FirstOrDefault(x => x.I_ID_PERSONA == request.I_PERSON_ID && x.B_ESTADO == "1");

            if (persona == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe la persona o está inactivo")
                    }
                };

            }
            string[] tipoCuenta = (request.V_TYPE_ACCOUNT_ID.Trim()).Split(',');
            var dni = _repositoryDocPersona.TableNoTracking.Where(x => x.I_ID_PERSONA == request.I_PERSON_ID && x.B_ESTADO == "1").FirstOrDefault()!.V_NRO_DOCUMENTO;
            //Creando numero de cuenta de 10 digitos
            int anio = DateTime.Now.Year %100;
            string numeroCuenta = "";

            if (_repositoryCuenta.TableNoTracking.Where(x => x.I_ID_PERSONA==request.I_PERSON_ID && x.B_ESTADO=="0").ToList().Count != 3)
            {

                foreach (string tipocuenta in tipoCuenta)
                {
                    if (0<Convert.ToInt32( tipocuenta) && Convert.ToInt32(tipocuenta) <= 3) { 
                        numeroCuenta = "0" + tipocuenta + dni + anio;
              
                        if (_repositoryCuenta.TableNoTracking.Where(x => x.V_NUMERO_CUENTA.Substring(0, 10) == numeroCuenta.Substring(0, 10)).ToList().Count == 0)
                        {
                            /// <summary>
                            /// insertar tabla Cuenta
                            /// </summary>
                            var newCuenta = new Cuenta
                            {
                                I_ID_PERSONA = request.I_PERSON_ID,
                                I_ID_TIPO_CUENTA = Convert.ToInt32(tipocuenta),
                                V_NUMERO_CUENTA = numeroCuenta,
                                I_SALDO = 0,
                                B_ESTADO = "1",
                                D_FECHA_CREACION = _repositoryDate.Now,
                                I_USUARIO_CREACION = usuario.I_ID_USUARIO
                            };
                            _repositoryCuenta.Insert(new List<Cuenta> { newCuenta });
                        }
                    }

                }
                /// <summary>
                /// insertar tabla puesto_persona
                /// </summary>

                if (_repositoryPuestoPers.TableNoTracking.Where(x => x.I_ID_PERSONA == request.I_PERSON_ID).ToList().Count == 0)
                {
                    var newPuestoPersona = new Puesto_persona
                    {
                        I_ID_PERSONA = request.I_PERSON_ID,
                        I_ID_PUESTO = 2,
                        B_ESTADO = "1",
                        D_FECHA_CREACION = _repositoryDate.Now,
                        I_USUARIO_CREACION = usuario.I_ID_USUARIO
                    };
                    _repositoryPuestoPers.Insert(new List<Puesto_persona> { newPuestoPersona });
                }
                await _unitOfWork.CommitChanges();
                return new SuccessResult<Unit>(Unit.Value);
            }
            return new FailureResult<IEnumerable<DetailError>>()
            {
                StatusCode = 400,
                Value = new List<DetailError>()
                {
                    new DetailError("06", "Tiene todas las cuentas creadas y estan inactivas")
                }
            };

        }
    }
}
